using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicTest
{
    public class UpAction : IAAction
    {
        public bool Execute()
        {
            Console.WriteLine("UpAction");
            Thread.Sleep(2000);
            Console.WriteLine("UpAction done.");
            return true;
        }

        public async Task<bool> ExecuteAsync()
        {
            //CancellationTokenSource cts = new CancellationTokenSource();
            bool result = false;
            result = await WorkingAsync();
            return result;
        }

        private async Task<bool> WorkingAsync()
        {
            Console.WriteLine("UpAction");
            await Task.Delay(8000);
            Console.WriteLine("UpAction done.");
            return true;
        }
    }
}
