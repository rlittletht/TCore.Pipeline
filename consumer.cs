using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TCore.Pipeline
{
    public class Consumer<T> where T : IPipelineBase<T>, new()
    {
        private SharedListenData<T> m_sld;

        public Consumer(SharedListenData<T> sld)
        {
            m_sld = sld;
        }

        void ProcessPendingRecords()
        {
            List<T> plt = m_sld.GrabListenRecords();

            m_sld.HookLog($"grabbed {plt.Count} records...");
            foreach (T t in plt)
                m_sld.HookListen(t);
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