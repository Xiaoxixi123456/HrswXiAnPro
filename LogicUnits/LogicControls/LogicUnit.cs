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
        private AutoResetEvent _offlineWaitFlag;

        [Bindable]
        public Tray CurrentTray { get; set; }
        [Bindable]
        public int CmmNo { get; set; }
        [Bindable]
        public string CmmName { get; set; }
        [Bindable]
        public bool CmmOnline { get; set; }
        [Bindable]
        public bool Working { get; set; }


        private ICMMControl _cmmControl;

        public LogicUnit(int cmmNo, string cmmName, ICMMControl cmmControl)
        {
            CmmNo = cmmNo;
            CmmName = cmmName;
            _cmmControl = cmmControl;
            Working = false;
            _actCtrl = new ActivityController() { Mark = true, IsOffline = false };
            _traySelector = new TraySelectActivity(cmmNo);
            _rootActivity = new RootActivity(cmmControl, _actCtrl);

            _offlineWaitFlag = new AutoResetEvent(false);
        }

        public async void CycleProcess(Rack _rack)
        {
            if (!CmmOnline) return;

            Working = true;
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
            // TODO 如果运行到这里，设置离线可能会断不开连接
            // 在cmmControl中增加标志位，是否还在连接
            //if (!CmmOnline)
            //{
            //    _cmmControl.Offline();
            //}
            Working = false;
            _offlineWaitFlag.Set();
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

        public async Task Offline()
        {
            if (!CmmOnline) return;

            _cts?.Cancel();
            _actCtrl.IsOffline = true;

            bool ok = await Task.Run(() =>
            {
                bool result = true;
                if (Working)
                {
                    result = _offlineWaitFlag.WaitOne(TimeSpan.FromMinutes(10));
                }
                return result;
            }).ConfigureAwait(false);
            if (!ok)
            {
                throw new TimeoutException("等待工作停止超时,无法离线!");
            }
            _cmmControl.Offline();
            CmmOnline = false;
        }

        public void Online()
        {
            if (CmmOnline) return;

            _actCtrl.IsOffline = false;

            if (!CmmOnline)
            {
                CmmOnline = _cmmControl.Online();
            }
        }
    }
}
