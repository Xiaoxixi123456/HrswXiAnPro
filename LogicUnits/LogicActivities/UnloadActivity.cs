using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using System.Threading;
using Hrsw.XiAnPro.PLCInteraction;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class UnloadActivity : IAActivity<Tray, bool>
    {
        UnloadControl _unloadControl;
        public UnloadActivity()
        {
            _unloadControl = new UnloadControl();
        }
        public void Complete()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            tray.Status = TrayStatus.TS_Unloading;
            return await _unloadControl.ExecuteAsync(tray);
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
