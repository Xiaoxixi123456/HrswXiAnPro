using System.Runtime.Serialization;

namespace Hrsw.XiAnPro.PCDmisServiceContracts
{
    [DataContract]
    public class PCDResponse
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public bool Pass { get; set; }
        [DataMember]
        public string ReportFile { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
