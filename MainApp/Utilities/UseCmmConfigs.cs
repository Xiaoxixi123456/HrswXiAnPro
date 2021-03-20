using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Utilities
{
    public class UseCmmConfigs : BindableBase
    {
        [Bindable]
        public bool UsePcdmis { get; set; }
        [Bindable]
        public bool UseCalypso { get; set; }
    }
}
