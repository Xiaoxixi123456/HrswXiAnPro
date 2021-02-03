using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PCDmisServiceContracts
{
    [ServiceContract]
    public interface IPCDmisCallback
    {
        [OperationContract]
        void SendMessage(PCDMessage response);
    }
}
