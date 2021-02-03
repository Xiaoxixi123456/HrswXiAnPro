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
        private ISelectorAActivity<Tray> _traySelector;
        private IAActivity<Tray> _rootActivity;

        [Bindable]
        public Tray CurrentTray { get; set; }
        [Bindable]
        public int CmmNo { get; set; }
        [Bindable]
        public string CmmName { get; set; }

        public LogicUnit(int cmmNo, ICMMControl cmmControl)
        {
            CmmNo = cmmNo;
            _traySelector = new SelectorActivity<Tray>();
            _rootActivity = new RootActivity(cmmControl);
        }

        public async void CycleProcess(Rack _rack)
        {
            _cts = new CancellationTokenSource();
            while (true)
            {
                if (_cts.IsCancellationRequested)
                    break;
                Tray tray = await _traySelector.ExecuteAsync(_rack.Trays, _cts);
                if (tray == null || _cts.IsCancellationRequested)
                    break;
                CurrentTray = tray;
                bool success = await _rootActivity.ExecuteAsync(CurrentTray, _cts);
                if (!success)
                    break;
            }
        }

        public void Stop()
        {
            _cts.Cancel();
        }
    }
}
