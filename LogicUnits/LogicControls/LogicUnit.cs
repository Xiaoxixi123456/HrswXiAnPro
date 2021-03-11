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
        private ActivityController _actCtrl;

        [Bindable]
        public Tray CurrentTray { get; set; }
        [Bindable]
        public int CmmNo { get; set; }
        [Bindable]
        public string CmmName { get; set; }
        [Bindable]
        public bool CmmOnline { get; set; }


        private ICMMControl _cmmControl;

        public LogicUnit(int cmmNo, string cmmName, ICMMControl cmmControl)
        {
            CmmNo = cmmNo;
            CmmName = cmmName;
            _cmmControl = cmmControl;
            CmmOnline = true;
            _actCtrl = new ActivityController() { Mark = true, IsOffline = false };
            _traySelector = new TraySelectActivity(cmmNo);
            _rootActivity = new RootActivity(cmmControl, _actCtrl);
        }

        public async void CycleProcess(Rack _rack)
        {
            if (!CmmOnline) return;

            _cts = new CancellationTokenSource();
            Tray tray = null;
            bool success;
            while (true)
            {
                if (_cts.IsCancellationRequested)
                    break;

                tray = await _traySelector.ExecuteAsync(_rack, _cts);
                if (tray == null) break;

                if (_cts.IsCancellationRequested)
                {
                    tray.Status = TrayStatus.TS_Idle;
                    break;
                }

                SetupTray(tray);

                success = await _rootActivity.ExecuteAsync(CurrentTray, _cts).ConfigureAwait(false);

                if (!success)
                {
                    // 报告错误并跳出循环
                    tray.Status = TrayStatus.TS_Error;
                    break;
                }
                tray.Status = TrayStatus.TS_Measured;
            }

            if (!CmmOnline)
            {
                _cmmControl.Offline();
            }
        }

        private void SetupTray(Tray tray)
        {
            CurrentTray = tray;
            CurrentTray.UseCmmNo = CmmNo;
        }

        public void NextPart()
        {
            _actCtrl.Mark = false;
            _actCtrl.Success = _cmmControl.ReleaseMeasure();
        }

        public void NextTray()
        {
            _actCtrl.Mark = null;
            _actCtrl.Success = _cmmControl.ReleaseMeasure();
        }

        public void Retry()
        {
            _actCtrl.Mark = true;
            _actCtrl.Success = _cmmControl.ReleaseMeasure();
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public void Offline()
        {
            _cts?.Cancel();
            _actCtrl.IsOffline = true;
            CmmOnline = false;
        }

        public void Online()
        {
            CmmOnline = true;
            _actCtrl.IsOffline = false;
            _cmmControl.Online();
        }
    }
}
