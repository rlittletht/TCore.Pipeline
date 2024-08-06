using System.Collections.Generic;
using System.Threading;
using System;

namespace TCore.Pipeline
{
    public class SharedListenData<T> where T : IPipelineWorkItemBase<T>, new()
    {
        // there is a single pipeline of records that are waiting to be processed
        private readonly LinkedList<T> m_listenerQueue;
        private int m_listenerQueueLength;
        private bool m_fDone;
        private readonly Object m_oLock;
        private readonly ManualResetEvent m_evt;
        private readonly int m_threadCount;
        private readonly int m_maxBatchSize;

        // this starts in the signaled state because the worker isn't processing records
        private readonly ManualResetEvent[] m_evtThreadWorking;
        private readonly WriteHookDelegate m_hook;
        private readonly Dictionary<int, int> m_mapIndexToThreadId = new Dictionary<int, int>();

        public CountdownEvent ThreadCountdown = new CountdownEvent(1);

        public SharedListenData(WriteHookDelegate hook, int threadCount, int maxBatchSize)
        {
            m_threadCount = threadCount;
            m_maxBatchSize = maxBatchSize;

            m_evtThreadWorking = new ManualResetEvent[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                m_evtThreadWorking[i] = new ManualResetEvent(true);
            }

            m_listenerQueue = new LinkedList<T>();
            m_listenerQueueLength = 0;
            m_fDone = false;
            m_oLock = new Object();
            // this is a single event that the multiple consumer threads
            // will wait on. it gets set whenever data is waiting, and is only
            // reset by a client when they completely empty the queue
            m_evt = new ManualResetEvent(false);
            m_hook = hook;
        }

        public int GetThreadIndexFromThreadId(int threadId)
        {
            for (int threadIndex = 0; threadIndex < m_mapIndexToThreadId.Count; threadIndex++)
            {
                if (m_mapIndexToThreadId[threadIndex] == threadId)
                    return threadIndex;
            }

            throw new Exception("unknown threadid");
        }

        public int GetThreadIdFromIndex(int threadIndex)
        {
            if (!m_mapIndexToThreadId.TryGetValue(threadIndex, out int threadId))
                throw new Exception("bad thread index");

            return threadId;
        }

        public void SetThreadIdForThreadIndex(int threadIndex, int threadId)
        {
            m_mapIndexToThreadId[threadIndex] = threadId;
        }

        public void HookLog(string sMessage)
        {
            if (m_hook != null)
                m_hook(sMessage);
        }

        public void HookListen(T t)
        {
            if (m_hook == null)
                return;

            m_hook(t.ToString());
        }

        public void AddListenRecordAtFront(T t)
        {
            lock (m_oLock)
            {
                m_listenerQueue.AddFirst(t);
                m_listenerQueueLength++;
                m_evt.Set(); //signal that there is data waiting
            }
        }

        public void AddListenRecord(T t)
        {
            lock (m_oLock)
            {
                m_listenerQueue.AddLast(t);
                m_listenerQueueLength++;
                m_evt.Set(); //signal that there is data waiting
            }
        }

        public void TerminateListener()
        {
            lock (m_oLock)
            {
                m_fDone = true;
                m_evt.Set();
            }

            // signal that we are done
            ThreadCountdown.Signal();

            // outside the lock, wait for workers to finish
            ThreadCountdown.Wait();
        }

        public void WaitForEventSignal()
        {
            m_evt.WaitOne();
        }

        /*----------------------------------------------------------------------------
            %%Function: SignalThreadWorking
            %%Qualified: TCore.Pipeline.SharedListenData<T>.SignalThreadWorking

            This resets the event for thread working. This means TerminateListener
            will wait for this event to be set before returning
        ----------------------------------------------------------------------------*/
        public void SignalThreadWorking()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            int threadIndex = GetThreadIndexFromThreadId(threadId);

            m_evtThreadWorking[threadIndex].Reset();
        }

        public void SignalThreadWorkerComplete()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            int threadIndex = GetThreadIndexFromThreadId(threadId);

            m_evtThreadWorking[threadIndex].Set();
        }

        public bool IsDone()
        {
            bool fDone = false;
            lock (m_oLock)
            {
                fDone = m_fDone;
            }

            return fDone;
        }

        /*----------------------------------------------------------------------------
            %%Function: Accelerate
            %%Qualified: TCore.Pipeline.SharedListenData<T>.Accelerate

            Accelerate all items in the current listener queue that have a matching
            cookie value (put them at the from of the list)
        ----------------------------------------------------------------------------*/
        public void Accelerate(Guid cookie)
        {
            lock (m_oLock)
            {
                LinkedListNode<T> current = m_listenerQueue.First;
                LinkedListNode<T> insertAfter = null;

                while (current != null)
                {
                    LinkedListNode<T> next = current.Next;

                    if (current.Value is IPipelineWorkItemBase<T> work && work.Cookie == cookie)
                    {
                        if (current != m_listenerQueue.First)
                        {
                            if (insertAfter == null)
                            {
                                m_listenerQueue.AddFirst(current.Value);
                                insertAfter = m_listenerQueue.First;
                            }
                            else
                            {
                                m_listenerQueue.AddAfter(insertAfter, current.Value);
                                insertAfter = insertAfter.Next;
                            }

                            m_listenerQueue.Remove(current);
                        }
                    }

                    current = next;
                }
            }
        }


        /*----------------------------------------------------------------------------
            %%Function: GrabListenRecords
            %%Qualified: TCore.Pipeline.SharedListenData<T>.GrabListenRecords

            We are going to grab a set of records. Calculate the number of recrods
            to grab based on the number of threads.

            Always grab at least 1 record.  (if there is only 1 thread, we will grab
            all of them
        ----------------------------------------------------------------------------*/
        public List<T> GrabListenRecords()
        {
            List<T> plt;

            lock (m_oLock)
            {
                if (m_listenerQueueLength == 0)
                    return new List<T>();

                plt = new List<T>(m_listenerQueueLength);
                int recordCount = Math.Max(1, m_listenerQueueLength / m_threadCount);

                if (m_maxBatchSize > 0)
                    recordCount = Math.Min(recordCount, m_maxBatchSize);

                while (recordCount-- > 0)
                {
                    LinkedListNode<T> current = m_listenerQueue.First;
                    if (current == null)
                        throw new Exception("m_listenerQueueLength doesn't match real length");

                    T tNew = new T();

                    tNew.InitFrom(current.Value);
                    plt.Add(tNew);

                    m_listenerQueue.RemoveFirst();
                    m_listenerQueueLength--;
                }

                if (m_listenerQueueLength == 0)
                {
                    if (m_listenerQueue.First != null)
                        throw new Exception("m_listenerQueueLength doesn't match real length");

                    // only reset the event when there is no data waiting
                    m_evt.Reset();
                }
                else if (m_listenerQueue.First == null)
                {
                    throw new Exception("m_listenerQueueLength doesn't match real length");
                }
            }

            return plt;
        }
    }
}
