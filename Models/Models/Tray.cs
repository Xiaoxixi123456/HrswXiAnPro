﻿using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace Hrsw.XiAnPro.Models
{
    public class Tray : BindableBase, IStatus
    {
        [Bindable]
        public int Id { get; set; }
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
        public int SlotNb { get; set; }
        [Bindable]
        public AAStatus Status { get; set; }
        [Bindable]
        public ObservableCollection<Part> Parts { get; set; }

        public Tray(int cols = 0, int rows = 0)
        {
            Random rand = new Random(DateTime.Now.Second);
            ColumnCount = cols;
            RowCount = rows;
            if (rand.Next(1, 100) % 2 == 0)
                Status = AAStatus.Empty;
            else
                Status = AAStatus.Idle;
            if (Status == AAStatus.Idle)
            {
                Parts = new ObservableCollection<Part>();
                for (int i = 0; i < ColumnCount * RowCount; i++)
                {
                    if (rand.Next(1, 100) % 2 == 0)
                        Parts.Add(new Part() { SlotNb = i, Status = AAStatus.Empty });
                    else
                        Parts.Add(new Part() { SlotNb = i, Status = AAStatus.Idle});
                }
            }
        }
    }
}
