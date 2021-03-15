using Hrsw.XiAnPro.Models;
using System.Runtime.Serialization;

namespace CalypsoServiceInterfaces
{
    [DataContract]
    public class CalypsoRequest
    {
        [DataMember]
        public Part Part { get; set; }
    }
}