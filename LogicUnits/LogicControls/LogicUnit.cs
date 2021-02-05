using Hrsw.XiAnPro.LogicActivities;
using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.LogicControls
{
    public class LogicUnit : BindableBase
    {
        private CancellationTokenSource _cts;
        private IAActivity<Rack, Tray> _traySelector;
        private IAActivity<Tray, bool> _rootActivity;

        [Bindable]
        public Tray CurrentTray { get; set; }
        [Bindable]
        public int CmmNo { get; set; }
        [Bindable]
        public string CmmName { get; set; }

        public LogicUnit(int cmmNo, string cmmName, ICMMControl cmmControl)
        {
            CmmNo = cmmNo;
            CmmName = cmmName;
            _traySelector = new TraySelectActivity(cmmNo);
            _rootActivity = new RootActivity(cmmControl);
        }

        public async void CycleProcess(Rack _rack)
        {
            _cts = new CancellationTokenSource();
            AActivityFlags flags = new AActivityFlags() { Next = true };
            AActivityFlags.IsExit = false;
            Tray tray = null;
            while (true)
            {
                if (_cts.IsCancellationRequested || AActivityFlags.IsExit)
                    break;
                if (flags.Next)
                    tray = await _traySelector.ExecuteAsync(_rack, _cts);
                if (tray == null 
                    || _cts.IsCancellationRequested
                    || AActivityFlags.IsExit)
                    break;
                CurrentTray = tray;
                flags = await _rootActivity.ExecuteAsync(CurrentTray, _cts);
                if (AActivityFlags.IsExit)
                    break;
            }
        }

        public void Stop()
        {
            _cts.Cancel();
        }
    }
}
