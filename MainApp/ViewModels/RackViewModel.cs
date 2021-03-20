using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.Utilities;
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
    public class RackViewModel : BindableBase
    {
        [Bindable]
        public Rack Rack { get; set; }
        [Bindable]
        public ObservableCollection<Tray> Trays { get; set; }
        [Bindable]
        public Tray SelectedTray { get; set; }

        public DelegateCommand LoadTrayToRackCommand { get; set; }
        public DelegateCommand UnloadTrayFromRackCommand { get; set; }

        public RackViewModel()
        {
            LoadTrayToRackCommand = new DelegateCommand(LoadTrayToRack);
            UnloadTrayFromRackCommand = new DelegateCommand(UnloadTrayFromRack);
        }

        private void LoadTrayToRack()
        {
            if (SelectedTray == null)
                return;
            // 测量过程中无法不能调整料库
            if (Rack.Status == RackStatus.RS_Busy)
            {
                return;
            }
            LoadTraysViewModel ltvm = new LoadTraysViewModel();
            ltvm.SelectedTrayInRack = SelectedTray;
            ltvm.Rack = Rack;
            ltvm.Trays.Clear();
            //var trays = Trays.Where(t => t.Status == TrayStatus.TS_Idle).ToList();
            var trays = Trays.Where(t => !t.Placed && t.PartCount != 0).ToList();
            foreach (var item in trays)
            {
                ltvm.Trays.Add(item);
            }
            LoadTrayToRackWindow ltWnd = new LoadTrayToRackWindow();
            ltWnd.DataContext = ltvm;
            ltWnd.ShowDialog();
        }

        private void UnloadTrayFromRack()
        {
                if (SelectedTray == null ||
                    SelectedTray.Status == TrayStatus.TS_Empty)
                    return;
                if (Rack.Status == RackStatus.RS_Busy)
                {
                    return;
                }
                int index = SelectedTray.SlotNb - 1;
                Tray tray = SelectedTray;
                //tray.Status = TrayStatus.TS_Idle;
                tray.SlotNb = -1;
                tray.Placed = false;
                Tray emptyTray = new Tray()
                {
                    SlotNb = index + 1,
                    Status = TrayStatus.TS_Empty
                };
                Rack.Trays[index] = emptyTray;
        }
    }
}
