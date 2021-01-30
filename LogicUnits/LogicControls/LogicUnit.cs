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
        private Rack _rack;
        private CancellationTokenSource _cts;
        private IAActivity<Rack, Tray> _traySelector;
        private IAActivity<Tray> _baseActivity;

        [Bindable]
        public Tray CurrentTray { get; set; }

        public LogicUnit(Rack rack, IAActivity<Rack, Tray> selector, IAActivity<Tray> executor)
        {
            _rack = rack;
            _traySelector = selector;
            _baseActivity = executor;
            CurrentTray = new Tray();
        }

        public async void CycleProcess()
        {
            _cts = new CancellationTokenSource();
            while (true)
            {
                if (_cts.IsCancellationRequested)
                    break;
                Tray tray = await _traySelector.ExecuteAsync(_rack, _cts);
                if (tray == null || _cts.IsCancellationRequested)
                    break;
                CurrentTray = tray;
                bool success = await _baseActivity.ExecuteAsync(CurrentTray, _cts);
                if (!success)
                    break;
            }
        }

        public void Stop()
        {
            //_traySelector?.Stop();
            //_logicExecutor?.Stop();
            _cts.Cancel();
        }
    }
}
