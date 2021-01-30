using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDataValidation
{
    public partial class Person : BindableBase
    {
        [BindableAspect]
        public string Name { get; set; }
        [BindableAspect]
        public int Age { get; set; }
    }
}
