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

namespace MainApp.ViewModels
{
    public class TraysOfRackViewModel : BindableBase
    {
        [Bindable]
        public Rack Rack { get; set; }
        [Bindable]
        public ObservableCollection<Tray> Trays { get; set; }
        [Bindable]
        public Tray SelectedTray { get; set; }
        [Bindable]
        public Tray SelectedTrayInRack { get; set; }

        public DelegateCommand LoadTraysCommand { get; set; }
        public DelegateCommand UnloadTraysCommand { get; set; }

        public TraysOfRackViewModel()
        {
            LoadTraysCommand = new DelegateCommand(LoadTrays);
            UnloadTraysCommand = new DelegateCommand(UnloadTrays);

            Trays = new ObservableCollection<Tray>();

        }

        private void UnloadTrays()
        {
            if (SelectedTrayInRack == null)
                return;
            if (SelectedTrayInRack.Status == TrayStatus.TS_Empty)
                return;
            int index = SelectedTrayInRack.SlotNb;
            SelectedTrayInRack.Status = TrayStatus.TS_Idle;
            SelectedTrayInRack.SlotNb = -1;
            Trays.Add(SelectedTrayInRack);
            Rack.Trays[index - 1] = new Tray()
            {
                Status = TrayStatus.TS_Empty,
                SlotNb = index,
            };
        }

        private void LoadTrays()
        {
            if (SelectedTray == null || SelectedTrayInRack == null)
                return;
            //SelectedTrayInRack = SelectedTray;
            int index = SelectedTrayInRack.SlotNb;
            Rack.Trays[index - 1] = SelectedTray;
            Rack.Trays[index - 1].Status = TrayStatus.TS_Placed;
            Rack.Trays[index - 1].SlotNb = index;
            Trays.Remove(SelectedTray);
        }
    }
}
