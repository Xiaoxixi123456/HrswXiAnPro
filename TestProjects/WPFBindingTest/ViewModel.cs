using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFBindingTest
{
    public class ViewModel : BindableBase
    {
        [Bindable]
        public Person Person { get; set; }
        [Bindable]
        public UserControl UserUI { get; set; }

        public ViewModel()
        {
            Person = new Person() { Name = "Abc", Age = 12 };
            UserUI = new UserControl1();
        }
    }
}
