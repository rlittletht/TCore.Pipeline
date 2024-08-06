using System.Collections.Generic;
using System.Threading;
using System;

namespace TCore.Pipeline
{
    /// <summary>
    /// Provides a Producer/consumer pipeline with 1 or more consumer threads. Work items on the queue
    /// will be executed FIFO unless items are specifically accelerated using Accelerate() method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProducerConsumer<T> where T : IPipelineWorkItemBase<T>, new()
    {
        private SharedListenData<T> m_sld;
        private Producer<T> m_prod;
        private Consumer<T> m_cons;
        private readonly Dictionary<int, Thread> m_consumers = new Dictionary<int, Thread>();
        private int m_threadCount;

        /// <summary>
        /// Create a ProducerConsumer pipeline
        /// </summary>
        /// <param name="threadCount">Count of consumer threads.</param>
        /// <param name="recordDelegate"></param>
        /// <param name="maxBatchSize">The maximum number of records to pull off the queue at a time. Batches will always
        /// be proportiate to the number of threads, and will always be at least 1 record long. Passing 0 means there is
        /// no limit.<br/>
        /// Use this to tune how long things stay in the listener queue before they are pulled -- this allows the host
        /// process to potentially accelerate items that are still in the listener queue. Once a workitem is pulled from
        /// the listener queue, there is no way to change its priority -- it will get processed in-order by the worker
        /// thread</param>
        /// <param name="hook">Optional hook delegate to allow logging messages to be dispatched</param>
        public ProducerConsumer(int threadCount, Consumer<T>.ProcessRecordDelegate recordDelegate, int maxBatchSize, WriteHookDelegate hook = null)
        {
            m_threadCount = threadCount;

            m_sld = new SharedListenData<T>(hook, threadCount, maxBatchSize);

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
