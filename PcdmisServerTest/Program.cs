using Hrsw.XiAnPro.PCDmisService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PcdmisServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PCDmisService service = new PCDmisService();
            using (ServiceHost host = new ServiceHost(service))
            {
                host.Opened += Host_Opened;
                host.Open();

                Console.WriteLine("...");

                Console.ReadLine();
            }
        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Server listen...");
        }
    }
}
