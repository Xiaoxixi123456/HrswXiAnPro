using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PCDmisImplement
{
    public class DimensionItem
    {
        public string Axis { get; set; }
        public double Measured { get; set; }
        public double Nominal { get; set; }
        public double Plus { get; set; } // 上公差
        public double Minus { get; set; } // 下公差
        public double Deviation { get; set; }
        public double OutTol { get; set; }
    }
}
