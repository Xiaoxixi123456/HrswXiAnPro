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

namespace MainApp.ViewModels
{
    public class LogicUnitViewModel : BindableBase
    {
        [Bindable]
        public LogicUnit LogicUnit { get; set; }

        public LogicUnitViewModel(int cmmNo, ICMMControl cmmClient)
        {
            LogicUnit = new LogicUnit(cmmNo, cmmClient);
        }
        
    }
}
