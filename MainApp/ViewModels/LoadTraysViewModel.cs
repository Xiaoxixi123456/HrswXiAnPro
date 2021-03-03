using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MainApp.ViewModels
{
    public class LoadTraysViewModel : BindableBase
    {
        [Bindable]
        public ObservableCollection<Tray> Trays { get; set; }
        [Bindable]
        public Rack Rack { get; set; }
        [Bindable]
        public Tray SelectedTrayInRack { get; set; }
        [Bindable]
        public Tray SelectedTray { get; set; }
        public DelegateCommand<Window> LoadTrayCommand { get; set; }
        public LoadTraysViewModel()
        {
            Trays = new ObservableCollection<Tray>();
            LoadTrayCommand = new DelegateCommand<Window>(LoadTray);
        }

        private void LoadTray(Window wd)
        {
            if (SelectedTrayInRack == null 
                || SelectedTray == null)
                return;
            int index = SelectedTrayInRack.SlotNb - 1;
            if (SelectedTrayInRack.Status != TrayStatus.TS_Empty)
            {
                SelectedTrayInRack.Status = TrayStatus.TS_Idle;
                SelectedTrayInRack.SlotNb = -1;
                SelectedTrayInRack.Placed = false;
            }
            Tray tray = SelectedTray;
            Rack.Trays[index] = tray;
            Rack.Trays[index].SlotNb = index + 1;
            Rack.Trays[index].Status = TrayStatus.TS_Idle;
            Rack.Trays[index].Placed = true;
            if (wd != null)
                wd.Close();
        }
    }
}
