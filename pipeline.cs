using System;
using System.Collections.Generic;
using System.Threading;

namespace TCore.Pipeline
{
    public delegate void WriteHookDelegate(string sMessage);

    public class ProducerConsumer
    {

    }

    public interface IPipelineBase<T>
    {
        void InitFrom(T t);
    }


    public class SharedListenData<T> where T : IPipelineBase<T>, new()
    {
        private List<T> m_plt;
        private bool m_fDone;
        private Object m_oLock;
        private AutoResetEvent m_evt;
        private WriteHookDelegate m_hook;

        public SharedListenData(WriteHookDelegate hook)
        {
            m_plt = new List<T>();
            m_fDone = false;
            m_oLock = new Object();
            m_evt = new AutoResetEvent(false);
            m_hook = hook;
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
        }

        public void WaitForEventSignal()
        {
            m_evt.WaitOne();
        }

        public bool IsDone()
        {
            bool fDone = false;
            lock (m_oLock)
                fDone = m_fDone;

            return fDone;
        }

        public List<T> GrabListenRecords()
        {
            List<T> plt = new List<T>(m_plt.Count);

            lock (m_oLock)
            {
                foreach (T t in m_plt)
                {
                    T tNew = new T();
                    tNew.InitFrom(t);
                    plt.Add(tNew);
                }

                m_plt.Clear();
            }

            return plt;
        }
    }

    public class ProducerConsumer<T> where T: IPipelineBase<T>, new()
    {
        private SharedListenData<T> m_sld;
        private Producer<T> m_prod;
        private Consumer<T> m_cons;
        private Thread m_threadConsumer;

        public ProducerConsumer(WriteHookDelegate hook = null, Consumer<T>.ProcessRecordDelegate recordDelegate = null)
        {
            m_sld = new SharedListenData<T>(hook);

            m_prod = new Producer<T>(m_sld);
            m_cons = new Consumer<T>(m_sld, recordDelegate);
        }

        public Producer<T> Producer => m_prod;
        public Consumer<T> Consumer => m_cons;

        private int m_cSuspendedThread;

        [Obsolete]
        public void TestSuspendConsumerThread()
        {
            if (++m_cSuspendedThread == 1)
                m_threadConsumer.Suspend();
        }

        [Obsolete]
        public void TestResumeConsumerThread()
        {
            if (m_cSuspendedThread == 0)
                throw new Exception("poorly nested suspend");

            m_cSuspendedThread--;
            if (m_cSuspendedThread == 0)
                m_threadConsumer.Resume();
        }

        public Producer<T> Start()
        {
            m_threadConsumer = new Thread(m_cons.Listen);
            m_threadConsumer.Start();

            return m_prod;
        }

        public void Stop()
        {
            m_sld.TerminateListener();
        }
    }
}
