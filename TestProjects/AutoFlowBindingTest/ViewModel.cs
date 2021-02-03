using Hrsw.XiAnPro.LogicActivities;
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

namespace AutoFlowBindingTest
{
    public class ViewModel : BindableBase
    {
        //[Bindable]
        public Rack Rack { get; set; }
        //[Bindable]
        public LogicUnit AFManager { get; set; }

        public ViewModel()
        {
            Rack = new Rack(1, 1);
            //Rack.Trays[0] = new Tray()
            //{
            //    Id = 1,
            //    SlotNb = 0,
            //    Category = 3,
            //    Status = TrayStatus.TS_Idle
            //};
            AFManager = new LogicUnit(Rack);
        }

        public void Start()
        {
           Task.Run(() =>  AFManager.CycleProcess());
        }
    }
}
