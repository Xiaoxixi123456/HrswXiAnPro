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
    public class TraySelectActivity : IAActivity<Rack, Tray>
    {
        private static object syncLock = new object();

        public Task<Tray> ExecuteAsync(Rack rack, CancellationTokenSource cts)
        {
            return Task.Run(() =>
            {
                Tray tray = null;
                lock (syncLock)
                {
                    foreach (var item in rack.Trays)
                    {
                        if (item.Status == TrayStatus.TS_Idle)
                        {
                            item.Status = TrayStatus.TS_Wait;
                            tray = item;
                            break;
                        }
                    }
                }
                return tray;
            });
        }
    }
}
