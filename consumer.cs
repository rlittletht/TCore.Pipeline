using System.Collections.Generic;

namespace TCore.Pipeline
{
    public class Consumer<T> where T : IPipelineWorkItemBase<T>, new()
    {
        public delegate bool ShouldAbortDelegate();
        public delegate void ProcessRecordDelegate(IEnumerable<T> t, ShouldAbortDelegate shouldAbort);

        private readonly SharedListenData<T> m_sharedData;
        private readonly ProcessRecordDelegate m_processRecord;

        public Consumer(SharedListenData<T> sharedData, ProcessRecordDelegate processRecord)
        {
            m_sharedData = sharedData;
            m_processRecord = processRecord;
        }

        /*----------------------------------------------------------------------------
            %%Function: ShouldAbort
            %%Qualified: TCore.Pipeline.Consumer<T>.ShouldAbort

            Should threads abort their processing and go back to the top of the
            listening loop?
        ----------------------------------------------------------------------------*/
        bool ShouldAbort()
        {
            return m_sharedData.IsDone();
        }

        /*----------------------------------------------------------------------------
            %%Function: ProcessPendingRecords
            %%Qualified: TCore.Pipeline.Consumer<T>.ProcessPendingRecords

            Pull some or all of the pending records and process them.
        ----------------------------------------------------------------------------*/
        void ProcessPendingRecords()
        {
            List<T> plt = m_sharedData.GrabListenRecords();

            if (plt.Count == 0)
            {
                m_sharedData.HookLog($"nothing to grab...");
                return;
            }

            m_sharedData.HookLog($"grabbed {plt.Count} records...");
            if (m_processRecord != null)
                m_processRecord(plt, ShouldAbort);
        }

        /*----------------------------------------------------------------------------
            %%Function: Listen
            %%Qualified: TCore.Pipeline.Consumer<T>.Listen

            the thread worker. listen for new records to be available, pull some or
            all of the records, and dispatch them.

            when the scheduler says we are done, then exit the while and signal
            our thread is complete
        ----------------------------------------------------------------------------*/
        public void Listen()
        {
            m_sharedData.ThreadCountdown.AddCount();

            try
            {
                while (!m_sharedData.IsDone())
                {
                    m_sharedData.WaitForEventSignal();
                    m_sharedData.SignalThreadWorking();
                    ProcessPendingRecords();
                    m_sharedData.SignalThreadWorkerComplete();
                }
            }
            finally
            {
                // signal that we are complete
                m_sharedData.ThreadCountdown.Signal();
            }
        }
    }
}
