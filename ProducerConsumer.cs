using System.Collections.Generic;
using System.Threading;
using System;

namespace TCore.Pipeline
{
    public class ProducerConsumer<T> where T : IPipelineBase<T>, new()
    {
        private SharedListenData<T> m_sld;
        private Producer<T> m_prod;
        private Consumer<T> m_cons;
        private readonly Dictionary<int, Thread> m_consumers = new Dictionary<int, Thread>();
        private int m_threadCount;

        public ProducerConsumer(int threadCount = 1, WriteHookDelegate hook = null, Consumer<T>.ProcessRecordDelegate recordDelegate = null)
        {
            m_threadCount = threadCount;

            m_sld = new SharedListenData<T>(hook, threadCount);

            m_prod = new Producer<T>(m_sld);
            m_cons = new Consumer<T>(m_sld, recordDelegate);
        }

        public Producer<T> Producer => m_prod;
        public Consumer<T> Consumer => m_cons;

        private readonly Dictionary<int, int> m_suspendedThreads = new Dictionary<int, int>();


        [Obsolete]
        void TestSuspendAllConsumerThreads()
        {
            foreach (int threadIndex in m_consumers.Keys)
            {
                TestSuspendConsumerThread(threadIndex);
            }
        }

        [Obsolete]
        public void TestSuspendConsumerThread(int threadIndex)
        {
            int threadId = m_sld.GetThreadIdFromIndex(threadIndex);

            if (m_suspendedThreads.TryGetValue(threadId, out int current))
                current = 0;

            if (++current == 1)
                m_consumers[threadId].Suspend();

            m_suspendedThreads[threadId] = current;
        }

        [Obsolete]
        void TestResumeAllConsumerThreads()
        {
            foreach (int threadIndex in m_suspendedThreads.Keys)
            {
                TestResumeConsumerThread(threadIndex);
            }
        }

        [Obsolete]
        public void TestResumeConsumerThread(int threadIndex)
        {
            int threadId = m_sld.GetThreadIdFromIndex(threadIndex);

            if (!m_suspendedThreads.TryGetValue(threadId, out int current))
                throw new Exception("poorly nested suspend");

            if (current == 0)
                throw new Exception("poorly nested suspend");

            current--;
            if (current == 0)
                m_consumers[threadIndex].Resume();

            m_suspendedThreads[threadId] = current;
        }

        /*----------------------------------------------------------------------------
            %%Function: Start
            %%Qualified: TCore.Pipeline.ProducerConsumer<T>.Start
        ----------------------------------------------------------------------------*/
        public Producer<T> Start()
        {
            lock (m_sld)
            {
                for (int threadIndex = 0; threadIndex < m_threadCount; threadIndex++)
                {
                    Thread thread = new Thread(m_cons.Listen);
                    m_consumers[thread.ManagedThreadId] = thread;
                    m_sld.SetThreadIdForThreadIndex(threadIndex, thread.ManagedThreadId);
                    thread.Start();
                }
            }
            return m_prod;
        }

        public void Stop()
        {
            m_sld.TerminateListener();
        }
    }
}
