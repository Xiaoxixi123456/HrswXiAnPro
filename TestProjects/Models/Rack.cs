using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.Models
{
    public class Rack : BindableBase
    {
        public int ColumnCount { get; set; } = 3;
        public int RowCount { get; set; } = 3;
    }
}
