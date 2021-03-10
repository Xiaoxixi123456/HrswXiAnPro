using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PCDmisServiceContracts
{
    [DataContract]
    public class PCDMessage
    {
        [DataMember]
        public bool Result { get; set; }
        [DataMember]
        public bool Error { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
