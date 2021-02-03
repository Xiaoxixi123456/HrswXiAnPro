using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class OperaCallback : ServiceReference1.IServiceContractCallback
    {
        public void Reponse()
        {
            Console.WriteLine("callback reponse");
        }
    }
}
