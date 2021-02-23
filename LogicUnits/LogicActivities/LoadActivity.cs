using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Hrsw.XiAnPro.Models;
using PLCServices;
using Hrsw.XiAnPro.PLCInteraction;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class LoadActivity : IAActivity<Tray, bool>
    {
        private LoadControl _loadControl;
        public LoadActivity()
        {
            _loadControl = new LoadControl();
        }

        public async Task<bool> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            tray.Status = TrayStatus.TS_Loading;
            bool success = await _loadControl.ExecuteAsync(tray);
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

        public void Complete()
        {
            throw new NotImplementedException();
        }
    }
}
