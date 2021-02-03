using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PCDmisImplement
{
    public class Dimension
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string Feat1 { get; set; }
        public string Feat2 { get; set; }
        public string Feat3 { get; set; }
        public bool IsOutTol { get; set; }
        public List<DimensionItem> DimensionData { get; set; }

        public Dimension()
        {
            DimensionData = new List<DimensionItem>();
        }
        public override string ToString()
        {
            return ID;
        }
    }
}
