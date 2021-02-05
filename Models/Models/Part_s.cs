﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.Models
{
    /// <summary>
    /// 工件存储类
    /// </summary>
    [Serializable]
    public class Part_s
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Category { get; set; }
        public string DrawingNo { get; set; }
        public int CmmNo { get; set; }
        public PartStatus Status { get; set; }
    }
}