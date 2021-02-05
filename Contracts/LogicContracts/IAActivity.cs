using Hrsw.XiAnPro.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.LogicContracts
{
    public interface IAActivityBase
    {

    }

    public interface IAActivity<T, TResult> : IAActivityBase
    {
        Task<TResult> ExecuteAsync(T obj, CancellationTokenSource cts);
        void Next();
        void Retry();
        void Complete();
    }

    public interface IAActivity<T> : IAActivity<T, bool>
    {

    }

    public interface ISelectorAActivity<T> : IAActivity<IEnumerable<T>, T>
    {

    }
}
