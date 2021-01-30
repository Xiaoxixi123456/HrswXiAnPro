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
    public class UnloadActivity : IAActivity<Tray>
    {
        public Task<bool> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            throw new NotImplementedException();
        }
    }
}
