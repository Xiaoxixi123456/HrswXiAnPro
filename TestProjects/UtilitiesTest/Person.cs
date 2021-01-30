using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharpWpf
{
    public class Person : BindableBase
    {
      
        [Bindable]
        public string Name { get; set; }
 

        [Bindable]
        public int Age { get; set; }


        [Bindable]
        public string BkColor { get; set; } = "Red";
    }

    [Serializable]
    public class Person_s
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string BkColor { get; set; }
    }
}
