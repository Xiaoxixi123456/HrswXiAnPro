using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicTest
{
    public class MeasAction : IAAction
    {
        Worker _worker = new Worker();
        AutoResetEvent are = new AutoResetEvent(false);
        bool _success = false;
        public MeasAction()
        {
            _worker.Completed += _worker_Completed;
            _worker.EvtError += _worker_EvtError;
        }

        private void _worker_EvtError(object sender, EventArgs e)
        {
            _success = false;
            are.Set();
        }

        private void _worker_Completed(object sender, EventArgs e)
        {
            _success = true;
            are.Set();
        }

        public bool Execute()
        {
            _success = false;
            Task.Run(() =>
            {
                _worker.Dowork();
            });
            are.WaitOne();
            return _success;
        }

        public async Task<bool> ExecuteAsync()
        {
            //CancellationTokenSource cts = new CancellationTokenSource();
            //return Task<bool>.Factory.StartNew(() =>
            //{
            //    Working();
            //    return true;
            //}, /*cts.Token,*/ TaskCreationOptions.LongRunning/*, TaskScheduler.Current*/);
            bool result = false;
            result = await WorkingAsync();
            return result;
        }

        private async Task<bool> WorkingAsync()
        {
            //Console.WriteLine("MeasAction ");
            //await Task.Delay(3000);
            //Console.WriteLine("MeasAction done.");
            //return true;

            //bool result = await _worker.DoworkAsync();
            //return result;

            await Task.Run(() =>
            {
                _worker.Dowork();
                are.WaitOne();
            });
            return _success;
        }
    }
}
