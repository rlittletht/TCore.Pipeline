using System;
using System.Collections.Generic;
using System.Threading;

namespace TCore.Pipeline
{
    public delegate void WriteHookDelegate(string sMessage);

    /// <summary>
    /// Every workitem in the pipeline must implement this interface. 
    /// </summary>
    /// <typeparam name="T">The clients workitem specific type</typeparam>
    public interface IPipelineWorkItemBase<T>
    {
        /// <summary>
        /// Used to initialize a workitem from a given workitem (used when transfering items from the listener queue to the
        /// </summary>
        /// <param name="t">The workitem to copy from</param>
        void InitFrom(T t);

        /// <summary>
        /// Used to identify this workitem (or group of workitems -- this value does not have to be unique
        /// in the queue). Currently used by Accelerate
        /// </summary>
        Guid Cookie { get; }
    }
}
