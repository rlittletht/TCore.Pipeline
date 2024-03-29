﻿
namespace TCore.Pipeline
{
    public class Producer<T> where T: IPipelineBase<T>, new()
    {
        private SharedListenData<T> m_sld;

        public Producer(SharedListenData<T> sld)
        {
            m_sld = sld;
        }

        public void QueueRecord(T t)
        {
            m_sld.AddListenRecord(t);
        }
    }
}