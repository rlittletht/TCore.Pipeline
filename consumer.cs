using System.Collections.Generic;

namespace TCore.Pipeline
{
    public class Consumer<T> where T : IPipelineBase<T>, new()
    {
        public delegate bool ShouldAbortDelegate();
        public delegate void ProcessRecordDelegate(IEnumerable<T> t, ShouldAbortDelegate shouldAbort);

        private SharedListenData<T> m_sld;
        private ProcessRecordDelegate m_processRecord;

        public Consumer(SharedListenData<T> sld, ProcessRecordDelegate processRecord)
        {
            m_sld = sld;
            m_processRecord = processRecord;
        }

        bool ShouldAbort()
        {
            return m_sld.IsDone();
        }

        void ProcessPendingRecords()
        {
            List<T> plt = m_sld.GrabListenRecords();

            m_sld.HookLog($"grabbed {plt.Count} records...");
            if (m_processRecord != null)
                m_processRecord(plt, ShouldAbort);
        }

        public void Listen()
        {
            while (!m_sld.IsDone())
            {
                m_sld.WaitForEventSignal();
                m_sld.SignalThreadWorking();
                ProcessPendingRecords();
                m_sld.SignalThreadWorkerComplete();
            }
        }
    }
}