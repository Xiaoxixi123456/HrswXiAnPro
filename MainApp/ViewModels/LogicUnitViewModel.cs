﻿using Hrsw.XiAnPro.LogicContracts;
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

        public DelegateCommand OfflineCommand { get; set; }
        public DelegateCommand OnlineCommand { get; set; }

        public LogicUnitViewModel(int cmmNo, string cmmName, ICMMControl cmmClient)
        {
            LogicUnit = new LogicUnit(cmmNo, cmmName, cmmClient);
            OfflineCommand = new DelegateCommand(CmmOffline).ObservesCanExecute(() => CanOffline);
            OnlineCommand = new DelegateCommand(CmmOnline).ObservesCanExecute(() => CanOnline);
        }

        private void CmmOnline()
        {
            MessageBox.Show("online");
            LogicUnit.Online();
            CanOnline = false;
            CanOffline = true;
        }

        private void CmmOffline()
        {
            MessageBox.Show("offline");
            LogicUnit.Offline();
            CanOffline = false;
            CanOnline = true;
        }
    }
}
