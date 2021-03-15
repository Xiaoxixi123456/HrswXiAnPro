using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoServiceInterfaces
{
    [ServiceContract]
    public interface ICalypsoService
    {
        [OperationContract]
        bool MakeOffsetFile(CalypsoRequest request);
    }
}
