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
    public class PartsOfTrayViewModel : BindableBase
    {
        [Bindable]
        public Tray Tray { get; set; }
        [Bindable]
        public ObservableCollection<Part> Parts { get; set; }
        [Bindable]
        public ObservableCollection<Part> SelectedParts { get; set; }
        [Bindable]
        public ObservableCollection<int> SelectedIndexsOfTray { get; set; }
        [Bindable]
        public ObservableCollection<Part> SelectedPartsOfTray { get; set; }
        [Bindable]
        public int SelectedNumOfPartsInTray { get; set; }
        [Bindable]
        public int SelectedNumOfParts { get; set; }

        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand UnloadCommand { get; set; }
        //public DelegateCommand<object> SelectionChangedCommand { get; set; }
        //public DelegateCommand<object> PartsSelectionChangedCommand { get; set; }

        public PartsOfTrayViewModel()
        {
            LoadCommand = new DelegateCommand(LoadParts);
            UnloadCommand = new DelegateCommand(UnloadParts);
            //SelectionChangedCommand = new DelegateCommand<object>(SelectionChanged);
            //PartsSelectionChangedCommand = new DelegateCommand<object>(PartsSelectionChanged);

            Parts = new ObservableCollection<Part>();
            SelectedParts = new ObservableCollection<Part>();
            SelectedIndexsOfTray = new ObservableCollection<int>();
            SelectedPartsOfTray = new ObservableCollection<Part>();
        }

        //private void PartsSelectionChanged(object obj)
        //{
        //    SelectedParts.Clear();
        //    var parts = (obj as ObservableCollection<object>).Cast<Part>();
        //    foreach (var item in parts)
        //    {
        //        SelectedParts.Add(item);
        //    }
        //}

        //private void SelectionChanged(object items)
        //{
        //    SelectedPartsOfTray.Clear();
        //    var parts = (items as ObservableCollection<object>).Cast<Part>();
        //    foreach (var item in parts)
        //    {
        //        SelectedPartsOfTray.Add(item);
        //    }
        //}

        private void UnloadParts()
        {
            if (SelectedPartsOfTray.Count == 0)
                return;
            var indexs = SelectedPartsOfTray.Select(p => p.SlotNb).ToList();
            foreach (var item in indexs)
            {
                if (Tray.Parts[item - 1].Status == PartStatus.PS_Empty)
                    continue;
                //Tray.Parts[item - 1].Status = PartStatus.PS_Idle;
                Tray.Parts[item - 1].SlotNb = -1;
                Tray.Parts[item - 1].TrayNb = -1;
                Tray.Parts[item - 1].Placed = false;
                Parts.Add(Tray.Parts[item - 1]);
                Tray.Parts[item - 1] = new Part()
                {
                    SlotNb = item,
                    Status = PartStatus.PS_Empty
                };
            }
            Tray.PartCount = Tray.Parts.Count(p => p.Status != PartStatus.PS_Empty);
        }

        private void LoadParts()
        {
            if (SelectedParts.Count != SelectedPartsOfTray.Count)
                return;
            List<int> indexs = new List<int>();
            foreach (var item in SelectedPartsOfTray)
            {
                indexs.Add(item.SlotNb);
            }
            int pos = 0;
            foreach (var index in indexs)
            {
                if (SelectedPartsOfTray[pos].Status != PartStatus.PS_Empty)
                {
                    SelectedPartsOfTray[pos].Status = PartStatus.PS_Idle;
                    SelectedPartsOfTray[pos].SlotNb = -1;
                    SelectedPartsOfTray[pos].TrayNb = -1;
                    SelectedPartsOfTray[pos].Placed = false;
                    Parts.Add(SelectedPartsOfTray[pos]);
                }
                Part pt = SelectedParts[pos];
                Tray.Parts[index - 1] = pt;
                Tray.Parts[index - 1].Status = PartStatus.PS_Idle;
                Tray.Parts[index - 1].SlotNb = index;
                Tray.Parts[index - 1].TrayNb = Tray.TrayNb;
                Tray.Parts[index - 1].Placed = true;
                Parts.Remove(pt);
            }
            if (indexs.Count > 0)
            {
                Tray.Status = TrayStatus.TS_Idle;
            }
            Tray.PartCount = Tray.Parts.Count(p => p.Status != PartStatus.PS_Empty);
        }
    }
}
