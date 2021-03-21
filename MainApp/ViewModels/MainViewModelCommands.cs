using ClientCommonMods;
using Hrsw.XiAnPro.Models;
using MainApp.Utilities;
using MainApp.ViewModels;
using MainApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MainApp.ViewModels
{
    public partial class MainViewModel : BindableBase
    {
        public DelegateCommand SelectCategoryCommand { get; set; }
        public DelegateCommand AddPartCommand { get; set; }
        public DelegateCommand DeletePartCommand { get; set; }
        public DelegateCommand DeleteAllPartsCommand { get; set; }
        public DelegateCommand AddTrayCommand { get; set; }
        public DelegateCommand DeleteTrayCommand { get; set; }
        public DelegateCommand ImportTraysCommand { get; set; }
        public DelegateCommand LoadPartsCommand { get; set; }
        public DelegateCommand LoadTraysCommand { get; set; }
        public DelegateCommand UnLoadTraysFromSlotCommand { get; set; }
        public DelegateCommand ReadPartsCommand { get; set; }
        public DelegateCommand StartAutoflowCommand { get; set; }
        public DelegateCommand StopAutoflowCommand { get; set; }

        // menu commands
        public DelegateCommand CmmsSetupCommand { get; set; }
        public DelegateCommand DirsSetupCommand { get; set; }
        public DelegateCommand SelectedCmmsCommand { get; set; }


        public void CreateCommands()
        {
            SelectCategoryCommand = new DelegateCommand(PartsUISelectCategory);
            AddPartCommand = new DelegateCommand(AddPart);
            DeletePartCommand = new DelegateCommand(DeletePart);
            DeleteAllPartsCommand = new DelegateCommand(DeleteAllParts);
            AddTrayCommand = new DelegateCommand(AddTray);
            DeleteTrayCommand = new DelegateCommand(DeleteTray);
            ImportTraysCommand = new DelegateCommand(ImportTrays);
            LoadPartsCommand = new DelegateCommand(LoadPartsToTray);
            LoadTraysCommand = new DelegateCommand(LoadTraysToRack).ObservesCanExecute(() => Stopped);
            UnLoadTraysFromSlotCommand = new DelegateCommand(UnloadTraysFromSlot).ObservesCanExecute(() => Stopped);
            ReadPartsCommand = new DelegateCommand(ReadParts);
            StartAutoflowCommand = new DelegateCommand(StartAutoflow).ObservesCanExecute(() => Stopped);
            StopAutoflowCommand = new DelegateCommand(StopAutoflow).ObservesCanExecute(() => Started);

            // menu
            CmmsSetupCommand = new DelegateCommand(CmmsSetup).ObservesCanExecute(() => Stopped);
            DirsSetupCommand = new DelegateCommand(DirsSetup).ObservesCanExecute(() => Stopped);
            SelectedCmmsCommand = new DelegateCommand(SelectedCmms).ObservesCanExecute(() => Stopped);
            // 
            MyEventAggregator.Inst.GetEvent<MainAndLogicUnitEvent>().Subscribe(StartCmmWork);
        }

        private void SelectedCmms()
        {
            if (ConfigManager.cmmConfigs.UsePcdmis)
            {
                AddPcdmisCmm();
            }
            else if (!ConfigManager.cmmConfigs.UsePcdmis)
            {
                DeletePcdmisCmm();
            }

            if (ConfigManager.cmmConfigs.UseCalypso)
            {
                //if (!LogicUnits.Any(p => p.LogicUnit.CmmName == "Calypso"))
                //{
                //    AddCmm(1, "Calypso");
                //}
                AddCalypsoCmm();
            }
            else if (!ConfigManager.cmmConfigs.UseCalypso)
            {
                DeleteCalypsoCmm();
            }
        }

        private async void DeleteCalypsoCmm()
        {
            try
            {
                var cmm = LogicUnits.First(p => p.LogicUnit.CmmName == "Calypso");
                if (cmm == null)
                    return;
                await cmm.LogicUnit.Offline();
                LogicUnits.Remove(cmm);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void AddCalypsoCmm()
        {
            if (!LogicUnits.Any(p => p.LogicUnit.CmmName == "Calypso"))
            {
                AddCmm(1, "Calypso");
            }
        }

        private async void DeletePcdmisCmm()
        {
            try
            {
                var cmm = LogicUnits.First(p => p.LogicUnit.CmmName == "Pcdmis");
                if (cmm == null)
                    return;
                await cmm.LogicUnit.Offline();
                LogicUnits.Remove(cmm);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void AddPcdmisCmm()
        {
            if (!LogicUnits.Any(p => p.LogicUnit.CmmName == "Pcdmis"))
            {
                AddCmm(0, "Pcdmis");
            }
        }

        private void DirsSetup()
        {
            DirsSetupWindow dsWnd = new DirsSetupWindow();
            dsWnd.Topmost = true;
            dsWnd.ShowDialog();
        }

        private void StartCmmWork(int cmmNo)
        {
            // if (LogicUnits[cmmNo].LogicUnit.Working || !Started)
            //     return;
            //LogicUnits[cmmNo].LogicUnit.CycleProcess(Racks[0]);
            LogicUnitViewModel cmmVm;
            try
            {
                cmmVm = LogicUnits.Where(p => p.LogicUnit.CmmNo == cmmNo).Single();
            }
            catch (Exception)
            {
                return;
            }
            
            if (cmmVm.LogicUnit.Working || !Started)
                return;
            cmmVm.LogicUnit.CycleProcess(Racks[0]);
        }

        private void CmmsSetup()
        {
            CmmConfigsWindow ccWnd = new CmmConfigsWindow();
            ccWnd.DataContext = this;
            ccWnd.Topmost = true;
            ccWnd.ShowDialog();
        }

        private void ImportTrays()
        {
            foreach (var tray in Trays)
            {
                if (tray.Placed) return;
                foreach (var part in tray.Parts)
                {
                    if (part.Status != PartStatus.PS_Empty
                        && part.Placed)
                    {
                        part.Placed = false;
                        part.SlotNb = -1;
                        part.TrayNb = -1;
                    }
                }
            }
            Trays = TraysRepository.LoadTrays();
        }

        private void UpdateTrays()
        {
            TraysRepository.UpdateTrays(Trays);
        }

        private async void StartAutoflow()
        {
            //Start();
            if (LogicUnits.Count == 0)
            {
                Started = false;
                Stopped = true;
                return;
            }
            Started = true;
            Stopped = false;
            Racks[0].Status = RackStatus.RS_Busy;
            //await Task.Factory.StartNew( async () =>
            //{
            foreach (var item in LogicUnits)
            { 
               item.LogicUnit.CycleProcess(Racks[0]);
            }

            await Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    bool stopped = true;
                    
                    foreach (var item in LogicUnits)
                    {
                        if (item.LogicUnit.Working)
                        {
                            stopped = false;
                        }
                    }
                    if (stopped) break;
                }
            }, TaskCreationOptions.LongRunning);
            // TODO 结束问题未解决
            Racks[0].Status = RackStatus.RS_Idle;
            Started = false;
            Stopped = true;
        }
        
        private async void StopAutoflow()
        {
            foreach (var item in LogicUnits)
            {
                item.LogicUnit.Stop();
            }
            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromMinutes(30));

            await Task.Run(() =>
            {
                while (true)
                {
                    bool stopped = true;
                    if (cts.IsCancellationRequested) break;
                    foreach (var item in LogicUnits)
                    {
                        if (item.LogicUnit.Working)
                        {
                            stopped = false;
                        }
                    }
                    if (stopped) break;
                }
            }, cts.Token);
            Racks[0].Status = RackStatus.RS_Idle;
            Started = false;
            Stopped = true;
        }

        private void UnloadTraysFromSlot()
        {
            if (SelectedTypeId == 0)
            {
                if (SelectedTrayInRack == null ||
                    SelectedTrayInRack.Status == TrayStatus.TS_Empty)
                    return;
                // 测量过程中不能调整料库
                if (Racks[0].Status == RackStatus.RS_Busy)
                {
                    return;
                }
                int index = SelectedTrayInRack.SlotNb - 1;
                Tray tray = SelectedTrayInRack;
                //tray.Status = TrayStatus.TS_Idle;
                tray.SlotNb = -1;
                tray.Placed = false;
                Tray emptyTray = new Tray() {
                    SlotNb = index + 1,
                    Status = TrayStatus.TS_Empty };
                Racks[0].Trays[index] = emptyTray;
            }
        }

        private void DeleteAllParts()
        {
            //须判断是否放置在料盘中
            foreach (var part in Parts)
            {
                if (part.Placed)
                {
                    MessageBox.Show("有未卸载零件,无法清除", "警告", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }
            Parts.Clear();
            if (SelectParts != null)
                SelectParts.Clear();
            CategoriesRefresh();
            PartsFileRepository.UpdateParts(Parts);
        }

        private void LoadTraysToRack()
        {
            if (SelectedTypeId == 1)
            {
                LoadTraysAll();
            }
            else if (SelectedTypeId == 0)
            {
                LoadTraysOneByOne();
            }
        }

        private void LoadTraysAll()
        {
            if (SelectedRack == null)
                return;
            // 测量过程中无法不能调整料库
            if (SelectedRack.Status == RackStatus.RS_Busy)
            {
                return;
            }
            TraysOfRackViewModel trvm = new TraysOfRackViewModel();
            trvm.Rack = SelectedRack;
            trvm.Trays.Clear();
            //var trays = Trays.Where(t => t.Status == TrayStatus.TS_Idle).ToList();
            var trays = Trays.Where(t => !t.Placed).ToList();
            foreach (var item in trays)
            {
                trvm.Trays.Add(item);
            }
            TraysOfRackWindow trWindow = new TraysOfRackWindow();
            trWindow.DataContext = trvm;
            trWindow.ShowDialog();
        }

        private void LoadTraysOneByOne()
        {
            if (SelectedTrayInRack == null)
                return;
            // 测量过程中无法不能调整料库
            if (Racks[0].Status == RackStatus.RS_Busy)
            {
                return;
            }
            LoadTraysViewModel ltvm = new LoadTraysViewModel();
            ltvm.SelectedTrayInRack = SelectedTrayInRack;
            ltvm.Rack = Racks[0];
            ltvm.Trays.Clear();
            //var trays = Trays.Where(t => t.Status == TrayStatus.TS_Idle).ToList();
            var trays = Trays.Where(t => !t.Placed && t.PartCount != 0).ToList();
            foreach (var item in trays)
            {
                ltvm.Trays.Add(item);
            }
            LoadTraysWindow ltWnd = new LoadTraysWindow();
            ltWnd.DataContext = ltvm;
            ltWnd.ShowDialog();
        }
        private void LoadPartsToTray()
        {
            if (SelectedTray == null)
                return;
            if (SelectedTray.Placed)
            {
                // 警告：料盘装载中，无法删除
                return;
            }
            PartsOfTrayViewModel partsOfTrayViewModel = new PartsOfTrayViewModel();
            partsOfTrayViewModel.Tray = SelectedTray;
            //var parts = Parts.Where(p => p.Category == SelectedTray.Category && p.Status == PartStatus.PS_Idle);
            var parts = Parts.Where(p => p.Category == SelectedTray.Category && !p.Placed && (p.CmmNo == 2 || SelectedTray.CmmNo == 2 || p.CmmNo == SelectedTray.CmmNo));
            foreach (var item in parts)
            {
                partsOfTrayViewModel.Parts.Add(item);
            }
            TrayLoadPartsWindow loadPartsWindow = new TrayLoadPartsWindow();
            loadPartsWindow.DataContext = partsOfTrayViewModel;
            loadPartsWindow.ShowDialog();
        }

        private void DeletePart()
        {
            if (SelectedPart == null)
                return;
            //if (SelectedPart.Status != PartStatus.PS_Idle)
            //{
            //    return;
            //}
            if (SelectedPart.Placed)
            {
                return;
            }
            Parts.Remove(SelectedPart);
            SelectParts.Remove(SelectedPart);
            CategoriesRefresh();
            PartsFileRepository.UpdateParts(Parts);
        }

        private void DeleteTray()
        {
            if (SelectedTray == null)
                return;
            //if (SelectedTray.Status != TrayStatus.TS_Idle)
            //{
            //    // 警告：料盘装载中，无法删除
            //    return;
            //}
            if (SelectedTray.Placed)
            {
                // 警告：料盘装载中，无法删除
                return;
            }
            foreach (var item in SelectedTray.Parts)
            {
                item.Placed = false;
                item.SlotNb = -1;
                item.TrayNb = -1;
            }
            Trays.Remove(SelectedTray);
            TraysRepository.UpdateTrays(Trays);
        }

        private void AddTray()
        {
            AddTrayWindow addTrayWindow = new AddTrayWindow();
            addTrayWindow.Trays = Trays;
            addTrayWindow.ShowDialog();
            TraysRepository.UpdateTrays(Trays);
        }

        private void AddPart()
        {
            CategoriesRefresh();
            AddPartWindow addPartWindow = new AddPartWindow();
            addPartWindow.Parts = Parts;
            addPartWindow.ShowDialog();
            CategoriesRefresh();
            PartsFileRepository.UpdateParts(Parts);
        }

        private void PartsUISelectCategory()
        {
            if (CurrentSelectCategory != "All")
            {
                int result;
                bool ok = int.TryParse(CurrentSelectCategory, out result);
                if (!ok)
                    return;
                var ps = Parts.Where(p => p.Category == result);
                SelectParts = new ObservableCollection<Part>(ps);
            }
            else
            {
                SelectParts = Parts;
            }
        }
    }
}
