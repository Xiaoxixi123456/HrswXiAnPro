using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

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
            //AActivities.Add(new UnloadActivity());
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExecuteAsync(Tray obj, CancellationTokenSource cts)
        {
            bool success = true;
            foreach (var activity in AActivities)
            {
                success = await activity.ExecuteAsync(obj, cts);
                if (!success)
                {
                    // TODO 如果是上下料过程返回false，错误不可恢复
                    // 测量料盘总返回true，错误内部处理
                    obj.Status = TrayStatus.TS_Error;
                    break;
                }
            }
            return success;
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
