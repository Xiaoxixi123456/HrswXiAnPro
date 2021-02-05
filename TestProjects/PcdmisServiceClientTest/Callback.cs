using PcdmisServiceClientTest.ServiceReference1;
using System.Diagnostics;

namespace PcdmisServiceClientTest
{
    public class Callback : IPCDmisServiceCallback
    {
        public void SendMessage(PCDMessage response)
        {
            Debug.WriteLine("call back...");
        }
    }
}
