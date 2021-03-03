using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.Models
{
    [Serializable]
    public class Tray_s
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        public int CmmNo { get; set; }
        public double ColumnOffset { get; set; }
        public double RowOffset { get; set; }
        public double BaseColumnOffset { get; set; }
        public double BaseRowOffset { get; set; }
        //public TrayStatus Status { get; set; }

    }
}
