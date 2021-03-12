﻿using Hrsw.XiAnPro.CMMClient;
using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.LogicControls;
using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MainApp.ViewModels
{
    public partial class MainViewModel : BindableBase, IDisposable
    {
        [Bindable]
        public ObservableCollection<LogicUnitViewModel> LogicUnits;
        private Dispatcher dispatcher;

        [Bindable]
        //public Rack Rack { get; set; }
        public ObservableCollection<Rack> Racks { get; set; }
        [Bindable]
        public ObservableCollection<Tray> Trays { get; set; }
        [Bindable]
        public ObservableCollection<Part> Parts { get; set; }
        [Bindable]
        public ObservableCollection<Part> SelectParts { get; set; }
        [Bindable]
        public ObservableCollection<string> Categories { get; set; }
        [Bindable]
        public string CurrentSelectCategory { get; set; }
        [Bindable]
        public Tray SelectedTray { get; set; }
        [Bindable]
        public Tray SelectedTrayInRack { get; set; }
        [Bindable]
        public int SelectedTypeId { get; set; }
        [Bindable]
        public Part SelectedPart { get; set; }
        [Bindable]
        public Rack SelectedRack { get; set; }

        public PcdmisClient PcdmisClient { get; set; }
        public CalypsoClient CalypsoClient { get; set; }

        public MainViewModel()
        {
            CreateCommands();

            Racks = new ObservableCollection<Rack>();
            Trays = new ObservableCollection<Tray>();
            Parts = new ObservableCollection<Part>();
            Categories = new ObservableCollection<string>();

            Racks.Add(new Rack(3, 3));
            CurrentSelectCategory = "All";
            CategoriesRefresh();
            LogicUnits = new ObservableCollection<LogicUnitViewModel>();
            PcdmisClient = PcdmisClient.Inst;
            CalypsoClient = CalypsoClient.Inst;
            Trays = TraysRepository.LoadTrays();
        }

        private void CategoriesRefresh()
        {
            Categories.Clear();
            var s = Parts.Select(p => p.Category).Distinct();
            foreach (var item in s)
            {
                Categories.Add(item.ToString());
            }
            Categories.Add("All");
            CurrentSelectCategory = "All";
        }

        public void Initial()
        {
            //PcdmisClient.Initial();
            //CalypsoClient.Initial();
            //LogicUnits.Add(new LogicUnitViewModel(0, "Pcdmis", PcdmisClient) { CanOffline = true, CanOnline = false });
            //LogicUnits.Add(new LogicUnitViewModel(1, "Calypso", CalypsoClient));
            AddCmm(0, "Pcdmis");
        }

        public void AddCmm(int cmmNo, string cmmName)
        {
            var use = LogicUnits.Any(p => p.LogicUnit.CmmNo == cmmNo);
            if (use) return;
            ICMMControl cmmCtrl = null;
            bool connected = false;
            if (string.Compare(cmmName, "Pcdmis", true) == 0)
            {
                cmmCtrl = PcdmisClient.Inst;
            }
            else
            {
                cmmCtrl = CalypsoClient.Inst;
            }
            connected = cmmCtrl.Online();
            LogicUnitViewModel luVm = new LogicUnitViewModel(cmmNo, cmmName, cmmCtrl);
            luVm.CanOnline = !connected;
            luVm.CanOffline = connected;

            LogicUnits.Add(luVm);
        }

        public void DeleteCmm(int cmmNo)
        {
            try
            {
                var luVm = LogicUnits.First(p => p.LogicUnit.CmmNo == cmmNo);
                if (luVm == null) return;

                // 
                if (luVm.LogicUnit.CmmOnline)
                {
                    MessageBox.Show("离线失败，无法移除三坐标!", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                LogicUnits.Remove(luVm);
            }
            catch (Exception)
            {
            }
           
        }

        internal void Start()
        {
            foreach (var item in LogicUnits)
            {
                item.LogicUnit.CycleProcess(Racks[0]);
            }
        }

        public void Dispose()
        {
            PcdmisClient.Dispose();
        }
    }
}
