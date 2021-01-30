using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class MeasurePartActivity : IAActivity<Part>
    {
        public Task<bool> ExecuteAsync(Part obj, CancellationTokenSource cts)
        {
            throw new NotImplementedException();
        }
    }
}
