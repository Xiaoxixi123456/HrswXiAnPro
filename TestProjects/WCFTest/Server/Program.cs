using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(OperaService)))
            {
                host.Opened += Host_Opened;
                host.Open();

                Console.ReadLine();
            }
        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Host is opened..");
        }
    }
}
