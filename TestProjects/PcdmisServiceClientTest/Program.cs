using PcdmisServiceClientTest.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PcdmisServiceClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Callback cb = new Callback();
            ServiceReference1.PCDmisServiceClient client = new ServiceReference1.PCDmisServiceClient(new System.ServiceModel.InstanceContext(cb));
            client.InnerDuplexChannel.Opened += InnerDuplexChannel_Opened;
            client.Open();
            Thread.Sleep(2000);

            PCDRequest request = new PCDRequest();
            request.Part = new ServiceReference1.Part();
            var response = client.MeasurePart(request);

            Console.WriteLine(response.Message + " " + response.Success.ToString());
            Console.WriteLine("------");
            Console.ReadLine();
        }

        private static void InnerDuplexChannel_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("opened...");
        }
    }
}
