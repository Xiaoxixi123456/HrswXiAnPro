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
    public class MainViewModel : BindableBase
    {
        [Bindable]
        public ObservableCollection<LogicUnitViewModel> LogicUnits;
        private Dispatcher dispatcher;

        [Bindable]
        public Rack Rack { get; set; }

        public PcdmisClient PcdmisClient { get; set; }
        public CalypsoClient CalypsoClient { get; set; }

        public MainViewModel()
        {
            Rack = new Rack(5, 5);
            LogicUnits = new ObservableCollection<LogicUnitViewModel>();
            PcdmisClient = PcdmisClient.Inst;
            CalypsoClient = CalypsoClient.Inst;
        }

        public void Initial()
        {
            PcdmisClient.Initial();
            CalypsoClient.Initial();
            LogicUnits.Add(new LogicUnitViewModel(0, "Pcdmis", PcdmisClient));
            //LogicUnits.Add(new LogicUnitViewModel(1, "Calypso", CalypsoClient));
        }

        internal void Start()
        {
            foreach (var item in LogicUnits)
            {
                item.LogicUnit.CycleProcess(Rack);
            }
        }
    }
}
