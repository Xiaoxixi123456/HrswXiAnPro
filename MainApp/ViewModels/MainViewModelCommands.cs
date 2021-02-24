using Hrsw.XiAnPro.Models;
using MainApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DelegateCommand LoadPartsCommand { get; set; }
        public DelegateCommand LoadTraysCommand { get; set; }
        public DelegateCommand UnLoadTraysFromSlotCommand { get; set; }
        public DelegateCommand ReadPartsCommand { get; set; }
        public DelegateCommand StartAutoflowCommand { get; set; }

        public void CreateCommands()
        {
            SelectCategoryCommand = new DelegateCommand(PartsUISelectCategory);
            AddPartCommand = new DelegateCommand(AddPart);
            DeletePartCommand = new DelegateCommand(DeletePart);
            DeleteAllPartsCommand = new DelegateCommand(DeleteAllParts);
            AddTrayCommand = new DelegateCommand(AddTray);
            DeleteTrayCommand = new DelegateCommand(DeleteTray);
            LoadPartsCommand = new DelegateCommand(LoadPartsToTray);
            LoadTraysCommand = new DelegateCommand(LoadTraysToRack);
            UnLoadTraysFromSlotCommand = new DelegateCommand(UnloadTraysFromSlot);
            ReadPartsCommand = new DelegateCommand(ReadParts);
            StartAutoflowCommand = new DelegateCommand(StartAutoflow);
        }

        private void StartAutoflow()
        {
            Start();
        }

        private void UnloadTraysFromSlot()
        {
            if (SelectedTypeId == 0)
            {
                if (SelectedTrayInRack == null ||
                    SelectedTrayInRack.Status == TrayStatus.TS_Empty)
                    return;
                // TODO 测量过程中无法不能调整料库
                if (Racks[0].Status == RackStatus.RS_Busy)
                {
                    return;
                }
                int index = SelectedTrayInRack.SlotNb - 1;
                Tray tray = SelectedTrayInRack;
                tray.Status = TrayStatus.TS_Idle;
                tray.SlotNb = -1;
                Tray emptyTray = new Tray() {
                    SlotNb = index + 1,
                    Status = TrayStatus.TS_Empty };
                Racks[0].Trays[index] = emptyTray;
            }
        }

        private void DeleteAllParts()
        {
            Parts.Clear();
            if (SelectParts != null)
                SelectParts.Clear();
            CategoriesRefresh();
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
            // TODO 测量过程中无法不能调整料库
            if (SelectedRack.Status == RackStatus.RS_Busy)
            {
                return;
            }
            TraysOfRackViewModel trvm = new TraysOfRackViewModel();
            trvm.Rack = SelectedRack;
            trvm.Trays.Clear();
            var trays = Trays.Where(t => t.Status == TrayStatus.TS_Idle).ToList();
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
            // TODO 测量过程中无法不能调整料库
            if (Racks[0].Status == RackStatus.RS_Busy)
            {
                return;
            }
            LoadTraysViewModel ltvm = new LoadTraysViewModel();
            ltvm.SelectedTrayInRack = SelectedTrayInRack;
            ltvm.Rack = Racks[0];
            ltvm.Trays.Clear();
            var trays = Trays.Where(t => t.Status == TrayStatus.TS_Idle).ToList();
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
            if (SelectedTray.Status != TrayStatus.TS_Idle)
            {
                // 警告：料盘装载中，无法删除
                return;
            }
            PartsOfTrayViewModel partsOfTrayViewModel = new PartsOfTrayViewModel();
            partsOfTrayViewModel.Tray = SelectedTray;
            var parts = Parts.Where(p => p.Category == SelectedTray.Category && p.Status == PartStatus.PS_Idle);
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
            if (SelectedPart.Status != PartStatus.PS_Idle)
            {
                return;
            }
            Parts.Remove(SelectedPart);
            SelectParts.Remove(SelectedPart);
            CategoriesRefresh();
        }

        private void DeleteTray()
        {
            if (SelectedTray == null)
                return;
            if (SelectedTray.Status != TrayStatus.TS_Idle)
            {
                // 警告：料盘装载中，无法删除
                return;
            }
            Trays.Remove(SelectedTray);
        }

        private void AddTray()
        {
            AddTrayWindow addTrayWindow = new AddTrayWindow();
            addTrayWindow.Trays = Trays;
            addTrayWindow.ShowDialog();
        }

        private void AddPart()
        {
            CategoriesRefresh();
            AddPartWindow addPartWindow = new AddPartWindow();
            addPartWindow.Parts = Parts;
            addPartWindow.ShowDialog();
            CategoriesRefresh();
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
