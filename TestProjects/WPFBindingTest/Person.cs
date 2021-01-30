using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBindingTest
{
    public class Person : BindableBase
    {
        [Bindable]
        public string Name { get; set; }
        [Bindable]
        public int Age { get; set; }
    }
}
