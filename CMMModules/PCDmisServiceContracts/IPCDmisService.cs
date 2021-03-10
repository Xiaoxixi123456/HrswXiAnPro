using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PCDmisServiceContracts
{
    [ServiceContract(CallbackContract = typeof(IPCDmisCallback))]
    public interface IPCDmisService
    {
        [OperationContract]
        PCDResponse MeasurePart(PCDRequest request);
        [OperationContract]
        void ReleaseMeasure();
        [OperationContract]
        PCDResponse GotoSafePostion();
        [OperationContract]
        PCDResponse Connect();
        [OperationContract]
        PCDResponse Disconnect();
    }
}
