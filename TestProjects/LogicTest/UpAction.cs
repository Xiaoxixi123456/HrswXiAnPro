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
        public Task<bool> Execute()
        {
            //CancellationTokenSource cts = new CancellationTokenSource();
            return Task<bool>.Factory.StartNew(() =>
            {
                Working();
                return true;
            }, /*cts.Token,*/ TaskCreationOptions.LongRunning/*, TaskScheduler.Current*/);
        }

        private void Working()
        {
            Thread.Sleep(2000);
        }
    }
}
