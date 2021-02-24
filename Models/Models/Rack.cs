using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading;

namespace Hrsw.XiAnPro.Models
{
    public class Rack : BindableBase
    {
        [Bindable]
        public ObservableCollection<Tray> Trays { get; set; }
        [Bindable]
        public int ColumnCount { get; set; }
        [Bindable]
        public int RowCount { get; set; }
        [Bindable]
        public RackStatus Status { get; set; }
        public Rack(int cols, int rows)
        {
            ColumnCount = cols;
            RowCount = rows;
            Status = RackStatus.RS_Idle;
            Trays = new ObservableCollection<Tray>();
            for (int i = 0; i < ColumnCount * RowCount; i++)
            {
                Trays.Add(new Tray()
                {
                    Status = TrayStatus.TS_Empty,
                    SlotNb = i + 1,
                });
                //Thread.Sleep(1000);
            }
        }
    }
}
