using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ViewModels
{
    public class TrayViewModel : BindableBase
    {
        [Bindable]
        public int Category { get; set; }
        [Bindable]
        public int TrayNb { get; set; }
        [Bindable]
        public int ColumnCount { get; set; }
        [Bindable]
        public int RowCount { get; set; }
        [Bindable]
        public double ColumnOffset { get; set; }
        [Bindable]
        public double RowOffset { get; set; }
        [Bindable]
        public double BaseColumnOffset { get; set; }
        [Bindable]
        public double BaseRowOffset { get; set; }
        [Bindable]
        public int CmmNo { get; set; }
    }
}
