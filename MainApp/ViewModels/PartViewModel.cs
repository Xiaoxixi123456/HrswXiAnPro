using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ViewModels
{
    public class PartViewModel : BindableBase
    {
        [Bindable]
        public string PartName { get; set; }
        [Bindable]
        public int Category { get; set; }
        [Bindable]
        public int StartId { get; set; }
        [Bindable]
        public int Count { get; set; }
        [Bindable]
        public int DefId { get; set; }
        [Bindable]
        public int CmmNo { get; set; } = 2;
    }
}
