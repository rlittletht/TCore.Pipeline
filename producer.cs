
using System;

namespace TCore.Pipeline
{
    public class Producer<T> where T: IPipelineWorkItemBase<T>, new()
    {
        private SharedListenData<T> m_sld;

        public Producer(SharedListenData<T> sld)
        {
            m_sld = sld;
        }

        public void QueueRecordFirst(T t)
        {
            m_sld.AddListenRecordAtFront(t);
        }

        public void QueueRecord(T t)
        {
            m_sld.AddListenRecord(t);
        }

        public void Accelerate(Guid cookie)
        {
            m_sld.Accelerate(cookie);
        }
    }
}