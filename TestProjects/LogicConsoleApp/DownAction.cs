using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicTest
{
    public class DownAction : IAAction
    {
        public bool Execute()
        {
            Console.WriteLine("DownAction ");
            Thread.Sleep(3000);
            Console.WriteLine("DownAction done.");
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
            Console.WriteLine("DownAction ");
            await Task.Delay(3000);
            Console.WriteLine("DownAction done.");
            return true;
        }
    }
}
