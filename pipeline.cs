using System;
using System.Collections.Generic;
using System.Threading;

namespace TCore.Pipeline
{
    public delegate void WriteHookDelegate(string sMessage);

    public interface IPipelineBase<T>
    {
        void InitFrom(T t);
    }
}
