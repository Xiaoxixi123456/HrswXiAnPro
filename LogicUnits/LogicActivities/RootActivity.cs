using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class RootActivity : IAActivity<Tray, AActivityFlags>
    {
        private List<IAActivity<Tray, AActivityFlags>> AActivities;

        public RootActivity(ICMMControl cmmControl)
        {
            AActivities = new List<IAActivity<Tray, AActivityFlags>>();
            //AActivities.Add(new LoadActivity());
            AActivities.Add(new MeasureTrayActivity(cmmControl));
        }

        public async Task<AActivityFlags> ExecuteAsync(Tray obj, CancellationTokenSource cts)
        {
            AActivityFlags flags = new AActivityFlags() { Next = true };
            foreach (var activity in AActivities)
            {
                // TODO 重新测量整个料盘
                // 不需要重复上下料
                // 上下料动作中判断料盘是否相等
                flags = await activity.ExecuteAsync(obj, cts);
                if (!flags.Next)
                    break;
            }
            return flags;
        }
    }
}
