using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.LogicControls;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.CMMClient;
using Hrsw.XiAnPro.Utilities;
using System.Windows.Controls;
using Prism.Commands;
using System.Windows;
using Prism.Events;
using MainApp.Utilities;
using ClientCommonMods;

namespace MainApp.ViewModels
{
    public class LogicUnitViewModel : BindableBase
    {
        [Bindable]
        public LogicUnit LogicUnit { get; set; }
        [Bindable]
        public bool CanOffline { get; set; }
        [Bindable]
        public bool CanOnline { get; set; }
        [Bindable]
        public bool Started { get; set; }
        [Bindable]
        public bool CmmError { get; set; }

        public DelegateCommand OfflineCommand { get; set; }
        public DelegateCommand OnlineCommand { get; set; }
        public DelegateCommand StartWorkflowCommand { get; set; }

        public LogicUnitViewModel(int cmmNo, string cmmName, ICMMControl cmmClient)
        {
            LogicUnit = new LogicUnit(cmmNo, cmmName, cmmClient);

            OfflineCommand = new DelegateCommand(CmmOffline).ObservesCanExecute(() => CanOffline);
            OnlineCommand = new DelegateCommand(CmmOnline).ObservesCanExecute(() => CanOnline);
            StartWorkflowCommand = new DelegateCommand(StartWorkflow).ObservesCanExecute(() => Started);

            cmmClient.OfflineEvent += CmmClient_OfflineEvent;
            LogicUnit.StartedEvent += LogicUnit_StartedEvent;
            LogicUnit.StoppedEvent += LogicUnit_StoppedEvent;
            MyEventAggregator.Inst.GetEvent<CmmErrorEvent>().Subscribe(OnCmmEvent);

            CmmError = false;
        }

        private void OnCmmEvent(bool err)
        {
            CmmError = err;
        }

        private void LogicUnit_StoppedEvent(object sender, EventArgs e)
        {
            Started = true;
        }

        private void LogicUnit_StartedEvent(object sender, EventArgs e)
        {
            Started = false;
        }

        private void StartWorkflow()
        {
            MyEventAggregator.Inst.GetEvent<MainAndLogicUnitEvent>().Publish(LogicUnit.CmmNo);
        }

        private void CmmClient_OfflineEvent(object sender, EventArgs e)
        {
            CanOnline = true;
            CanOffline = false;
            CmmError = false;
            LogicUnit.CmmOnline = false;
        }

        private void CmmOnline()
        {
            CanOnline = false;
            CmmError = false;
            LogicUnit.Online();
            if (LogicUnit.CmmOnline)
            {
                CanOffline = true;
            }
            else
            {
                CanOnline = true;
            }
        }

        private async void CmmOffline()
        {
            CanOffline = false;
            CmmError = false;
            try
            {
                await LogicUnit.Offline();
            }
            catch (Exception)
            {
                // TODO 记录超时无法离线情况
                CanOffline = true;
                return;
            }
            CanOnline = true;
        }
    }
}
