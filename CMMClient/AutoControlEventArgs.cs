﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.CMMClients
{
    public class AutoControlEventArgs : EventArgs
    {
        public string EventType { get; set; }
        public string Message { get; set; }
        public DateTime EvtTime { get; set; }
    }
}
