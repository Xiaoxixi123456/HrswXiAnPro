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
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        public Rack(int cols, int rows)
        {
            ColumnCount = cols;
            RowCount = rows;
            Trays = new ObservableCollection<Tray>();
            for (int i = 0; i < ColumnCount * RowCount; i++)
            {
                Trays.Add(new Tray(3, 3) { Id = i, SlotNb = i });
                Thread.Sleep(1000);
            }
        }
    }
}
