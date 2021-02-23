using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.Models
{
    public class Part : BindableBase
    {
        [Bindable]
        public int Id { get; set; }
        [Bindable]
        public string Name { get; set; }
        [Bindable]
        public int PartNb { get; set; }
        [Bindable]
        public int Category { get; set; }
        [Bindable]
        public string DrawingNo { get; set; }
        [Bindable]
        public int CmmNo { get; set; }
        [Bindable]
        public int UseCmmNo { get; set; }
        [Bindable]
        public int TrayNb { get; set; }
        [Bindable]
        public int SlotNb { get; set; }
        [Bindable]
        public bool Pass { get; set; }
        [Bindable]
        public double XOffset { get; set; }
        [Bindable]
        public double YOffset { get; set; }
        [Bindable]
        public int Flag { get; set; }
        [Bindable]
        public PartStatus Status { get; set; }
    }
}
