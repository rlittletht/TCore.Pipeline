using System.Collections.Generic;
using System.Threading;
using System;

namespace TCore.Pipeline
{
    public class SharedListenData<T> where T : IPipelineBase<T>, new()
    {
        // there is a single pipeline of records that are waiting to be processed
        private List<T> m_plt;
        private bool m_fDone;
        private Object m_oLock;
        private ManualResetEvent m_evt;

        private int m_threadCount;

        // this starts in the signaled state because the worker isn't processing records
        private readonly ManualResetEvent[] m_evtThreadWorking;
        private readonly WriteHookDelegate m_hook;
        private readonly Dictionary<int, int> m_mapIndexToThreadId = new Dictionary<int, int>();

        public CountdownEvent ThreadCountdown = new CountdownEvent(1);

        public SharedListenData(WriteHookDelegate hook, int threadCount)
        {
            m_threadCount = threadCount;

            m_evtThreadWorking = new ManualResetEvent[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                m_evtThreadWorking[i] = new ManualResetEvent(true);
            }

            m_plt = new List<T>();
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

        public void AddListenRecord(T t)
        {
            lock (m_oLock)
            {
                m_plt.Add(t);
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
                if (m_plt.Count == 0)
                    return new List<T>();

                plt = new List<T>(m_plt.Count);
                int recordCount = Math.Max(1, m_plt.Count / m_threadCount);

                for (int i = 0; i < recordCount; i++)
                {
                    T t = m_plt[i];

                    T tNew = new T();
                    tNew.InitFrom(t);
                    plt.Add(tNew);
                }

                if (recordCount < m_plt.Count)
                {
                    m_plt.RemoveRange(0, recordCount);
                }
                else
                {
                    m_plt.Clear();

                    // only reset the event when there is no data waiting
                    m_evt.Reset();
                }
            }

            return plt;
        }
    }
}
