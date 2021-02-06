using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using System.Threading;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class UnloadActivity : IAActivity<Tray, bool>
    {
        public void Complete()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            throw new NotImplementedException();
        }

        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Retry()
        {
            throw new NotImplementedException();
        }
    }
}
