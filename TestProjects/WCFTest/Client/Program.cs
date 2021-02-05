using Client.ServiceReference1;
using Contracts;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            OperaCallback ock = new OperaCallback();
            var context = new InstanceContext(ock);
            ServiceContractClient scc = new ServiceContractClient(context);
            scc.Open();
            scc.InnerChannel.Faulted += InnerDuplexChannel_Faulted;
            scc.InnerDuplexChannel.Faulted += InnerDuplexChannel_Faulted1;

            scc.Hook();

            bool success = scc.Operator();
            string str;
            try
            {
                str = scc.ReturnString();
            }
            catch (CommunicationException ce)
            {
                str = ce.Message;
                scc.Abort();
                scc = new ServiceContractClient(context);
                scc.Open();
                //scc.Close();
                //scc.Open();
                //Console.WriteLine(ce.Message);
            }
            OpRequest request = new OpRequest();
            request.part = new Part() { Name = "abb", Id = 20 };
            string name = scc.Send(request);

            Console.WriteLine("suc " + success.ToString());
            Console.WriteLine(str);
            Console.WriteLine(name);
            if (scc.State != CommunicationState.Faulted)
            {
                scc.Close();
            }
            else
            {
                scc.Abort();
            }

            Console.ReadLine();
        }

        private static void InnerDuplexChannel_Faulted1(object sender, EventArgs e)
        {
            Console.WriteLine("Duplex Timeout...");
        }

        private static void InnerDuplexChannel_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("Timeout...");
        }
    }
}
