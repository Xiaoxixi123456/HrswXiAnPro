using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlowBindingTest
{
    public class TestBindable : BindableBase
    {
        [Bindable]
        public Tray Tray { get; set; }
        [Bindable]
        public int xxx { get; set; }
        public TestBindable()
        {
            Tray = new Tray() { Id = 2 };
            xxx = 500;
        }
    }
}
