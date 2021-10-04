using System.Collections.Generic;

namespace TCore.Pipeline
{
    public class Consumer<T> where T : IPipelineBase<T>, new()
    {
        public delegate void ProcessRecordDelegate(IEnumerable<T> t);

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
            if (m_processRecord != null)
                m_processRecord(plt);
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