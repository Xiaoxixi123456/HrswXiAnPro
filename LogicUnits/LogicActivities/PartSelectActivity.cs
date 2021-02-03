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
    public class PartSelectActivity : IAActivity<Tray, Part>
    {
        public static object syncLock = new object();

        public Task<Part> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            return Task.Run(() =>
            {
                Part part = null;
                lock (syncLock)
                {
                    foreach (var item in tray.Parts)
                    {
                        if (item.Status == AAStatus.Idle)
                        {
                            item.Status = AAStatus.Wait;
                            part = item;
                            break;
                        }
                    }
                }
                return part;
            });
        }
    }
}
