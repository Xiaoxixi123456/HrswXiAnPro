using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace Hrsw.XiAnPro.Models
{
    public class Tray : BindableBase
    {
        [Bindable]
        public int Id { get; set; }
        [Bindable]
        public int TrayNb { get; set; }
        [Bindable]
        public int Category { get; set; }
        [Bindable]
        public int ColumnCount { get; set; }
        [Bindable]
        public int RowCount { get; set; }
        [Bindable]
        public double ColumnOffset { get; set; }
        [Bindable]
        public double RowOffset { get; set; }
        [Bindable]
        public double BaseColumnOffset { get; set; }
        [Bindable]
        public double BaseRowOffset { get; set; }
        [Bindable]
        public int CmmNo { get; set; }
        [Bindable]
        public int UseCmmNo { get; set; }
        [Bindable]
        public int PartCount { get; set; }
        [Bindable]
        public int SlotNb { get; set; }
        [Bindable]
        public bool Placed { get; set; }
        //[Bindable]
        //public int FixCategory { get; set; }
        [Bindable]
        public TrayStatus Status { get; set; }
        [Bindable]
        public ObservableCollection<Part> Parts { get; set; }

        public Tray(int cols = 0, int rows = 0)
        {
            Parts = new ObservableCollection<Part>();
            for (int i = 0; i < ColumnCount * RowCount; i++)
            {
                Parts.Add(new Part()
                {
                    SlotNb = i + 1,
                    Status = PartStatus.PS_Empty,
                });
            }
            PartCount = 0;
        }

        public void MakePartPlaceholder()
        {
            Status = TrayStatus.TS_Idle;
            Placed = false;
            UseCmmNo = -1;
            PartCount = 0;
            Parts = new ObservableCollection<Part>();
            for (int i = 0; i < ColumnCount * RowCount; i++)
            {
                Parts.Add(new Part()
                {
                    SlotNb = i + 1,
                    Status = PartStatus.PS_Empty,
                });
            }
        }
    }
}
