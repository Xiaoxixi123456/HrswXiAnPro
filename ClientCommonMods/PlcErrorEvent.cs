using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCommonMods
{
    public class PlcErrorStatus
    {
        //public bool LoadOrUnload { get; set; }
        public bool Error { get; set; }
    }

    public class PlcErrorEvent : PubSubEvent<PlcErrorStatus>
    {
    }
}
