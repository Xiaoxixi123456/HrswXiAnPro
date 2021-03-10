using Hrsw.XiAnPro.CMMClient;
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
using System.Windows.Threading;

namespace MainApp.ViewModels
{
    public partial class MainViewModel : BindableBase
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
            PcdmisClient.Initial();
            //CalypsoClient.Initial();
            LogicUnits.Add(new LogicUnitViewModel(0, "Pcdmis", PcdmisClient));
            //LogicUnits.Add(new LogicUnitViewModel(1, "Calypso", CalypsoClient));
        }

        internal void Start()
        {
            foreach (var item in LogicUnits)
            {
                item.LogicUnit.CycleProcess(Racks[0]);
            }
        }
    }
}
