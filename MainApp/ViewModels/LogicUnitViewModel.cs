using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.LogicControls;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.CMMClient;
using Hrsw.XiAnPro.Utilities;
using System.Windows.Controls;

namespace MainApp.ViewModels
{
    public class LogicUnitViewModel : BindableBase
    {
        [Bindable]
        public LogicUnit LogicUnit { get; set; }

        //[Bindable]
        //public UserControl LogicUnitUI { get; set; }

        public LogicUnitViewModel(int cmmNo, string cmmName, ICMMControl cmmClient)
        {
            LogicUnit = new LogicUnit(cmmNo, cmmName, cmmClient);
        }
        

    }
}
