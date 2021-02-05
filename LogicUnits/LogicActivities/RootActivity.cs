﻿using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class RootActivity : IAActivity<Tray, bool>
    {
        private List<IAActivity<Tray, bool>> AActivities;

        public RootActivity(ICMMControl cmmControl)
        {
            AActivities = new List<IAActivity<Tray, bool>>();
            //AActivities.Add(new LoadActivity());
            AActivities.Add(new MeasureTrayActivity(cmmControl));
        }

        public async Task<bool> ExecuteAsync(Tray obj, CancellationTokenSource cts)
        {
            bool success = true;
            foreach (var activity in AActivities)
            {
                // TODO 重新测量整个料盘
                // 不需要重复上下料
                // 上下料动作中判断料盘是否相等
                success = await activity.ExecuteAsync(obj, cts);

            }
            return success;
        }
    }
}
