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
    public class RootActivity : IAActivity<Tray>
    {
        private List<IAActivity<Tray>> AActivities;

        public RootActivity()
        {
            AActivities = new List<IAActivity<Tray>>();
            AActivities.Add(new LoadActivity());
        }

        public async Task<bool> ExecuteAsync(Tray obj, CancellationTokenSource cts)
        {
            bool result = false;
            foreach (var activity in AActivities)
            {
                result = await activity.ExecuteAsync(obj, cts);
                if (!result)
                    break;
            }
            return result;
        }
    }
}
