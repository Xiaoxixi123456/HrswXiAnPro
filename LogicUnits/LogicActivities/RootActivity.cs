using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using ClientCommonMods;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class RootActivity : IAActivity<Tray, bool>
    {
        private List<IAActivity<Tray, bool>> AActivities;

        public RootActivity(ICMMControl cmmControl, ActivityController ac)
        {
            AActivities = new List<IAActivity<Tray, bool>>();
            //AActivities.Add(new LoadActivity());
            AActivities.Add(new MeasureTrayActivity(cmmControl, ac));
            AActivities.Add(new SafeLocateActivity(cmmControl));
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
                try
                {
                    success = await activity.ExecuteAsync(obj, cts);
                    if (!success)
                    {
                        // 如果是上下料过程返回false，错误不可恢复
                        // 测量料盘时断开与服务器的连接，返回false
                        // 其他情况下都是返回true
                        obj.Status = TrayStatus.TS_Error;
                        break;
                    }
                }
                catch (Exception)
                {
                    obj.Status = TrayStatus.TS_Error;
                    // TODO 测量错误后料盘是否可以继续下料
                    ClientLogs.Inst.AddLog(new ClientLog("料盘测量错误."));
                    success = false;
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
