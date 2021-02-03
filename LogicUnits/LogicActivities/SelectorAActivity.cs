using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Hrsw.XiAnPro.Models;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class SelectorActivity<T> : ISelectorAActivity<T>
        where T : IStatus
    {
        public Task<T> ExecuteAsync(IEnumerable<T> collection, CancellationTokenSource cts)
        {
            return Task.Run(() =>
           {
               T item = default(T);
               foreach (var c in collection)
               {
                   if (c.Status == AAStatus.Idle)
                   {
                       c.Status = AAStatus.Wait;
                       item = c;
                       break;
                   }
               }
               return item;
           });
        }
    }
}
