using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.Models
{
    [Serializable]
    public class TraySer
    {
        public ObservableCollection<PartSer> Parts { get; set; }
        public string Name { get; set; }
    }
}
