using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class OperaService : IServiceContract
    {
        private  ICallbackContract _callback;
        public OperaService()
        {
            OperationContext.Current.Channel.Faulted += Channel_Faulted;
            OperationContext.Current.Channel.Closed += Channel_Closed;
        }

        private void Channel_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("client is closed");
        }

        private void Channel_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("Timeout....");
        }

        public void Hook()
        {
          _callback = OperationContext.Current.GetCallbackChannel<ICallbackContract>();
        }

        public bool Operator()
        {
            _callback.Reponse();
            return true;
        }

        public string ReturnString()
        {
            Thread.Sleep(TimeSpan.FromSeconds(30));
            return "测试";
        }

        public string Send(OpRequest request)
        {
            return request.part.Name;
        }
    }
}
