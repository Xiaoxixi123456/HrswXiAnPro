using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.Models
{
    public class Tray : BindableBase
    {
        private ObservableCollection<Part> _parts = new ObservableCollection<Part>();
        public ObservableCollection<Part> Parts
        {
            get { return _parts; }
            set { SetProperty(ref _parts, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}
