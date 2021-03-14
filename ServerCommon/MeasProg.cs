using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PCDmisService
{
    public class MeasProg : BindableBase
    {
        [Bindable]
        public int Id { get; set; }
        [Bindable]
        public string FileName { get; set; }
    }
}
