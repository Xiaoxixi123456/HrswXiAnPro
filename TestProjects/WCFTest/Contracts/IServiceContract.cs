using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract(CallbackContract = typeof(ICallbackContract))]
    public interface IServiceContract
    {
        [OperationContract]
        void Hook();
        [OperationContract]
        bool Operator();
        [OperationContract]
        string ReturnString();
        [OperationContract]
        string Send(OpRequest request);
    }

    [DataContract]
    public class OpRequest
    {
        [DataMember]
        public Part part { get; set; }
    }
}
