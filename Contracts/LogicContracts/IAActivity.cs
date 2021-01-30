using Hrsw.XiAnPro.Models;
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
    }

    public interface IAActivity<T> : IAActivity<T, bool>
    {

    }
}
