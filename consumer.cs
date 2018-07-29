using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TCore.Pipeline
{
    public class Consumer<T> where T : IPipelineBase<T>, new()
    {
        public delegate void ProcessRecordDelegate(T t);

        private SharedListenData<T> m_sld;
        private ProcessRecordDelegate m_processRecord;

        public Consumer(SharedListenData<T> sld, ProcessRecordDelegate processRecord)
        {
            m_sld = sld;
            m_processRecord = processRecord;
        }

        void ProcessPendingRecords()
        {
            List<T> plt = m_sld.GrabListenRecords();

            m_sld.HookLog($"grabbed {plt.Count} records...");
            foreach (T t in plt)
            {
                m_sld.HookListen(t);

                // at this point we have to actually do something. call the delegate to do
                // this
                if (m_processRecord != null)
                    m_processRecord(t);
            }
        }

        public void Listen()
        {
            while (!m_sld.IsDone())
            {
                m_sld.WaitForEventSignal();
                ProcessPendingRecords();
            }
        }
    }
}