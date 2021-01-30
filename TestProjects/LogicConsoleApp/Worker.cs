using System;
using System.Threading;
using System.Threading.Tasks;

namespace LogicTest
{
    internal class Worker
    {
        public event EventHandler Completed;
        public event EventHandler EvtError;

        public void Dowork()
        {
            Console.WriteLine("Dowork....");
            Thread.Sleep(1000);
            Random rand = new Random(DateTime.Now.Millisecond);
            int x = rand.Next(2);
            Console.WriteLine("Work done.");
            if (x == 0)
            {
                Completed?.Invoke(this, null);
            }
            else
            {
                EvtError?.Invoke(this, null);
            }
        }

        /// <summary>
        /// PCDMIS 异步执行，出错如何处理
        /// </summary>
        /// <returns></returns>
        internal Task<bool> DoworkAsync()
        {
           return Task.Run(() =>
            {
                Thread.Sleep(2000);
                Random rand = new Random(DateTime.Now.Millisecond);
                int x = rand.Next(2);
                if (x == 0)
                {
                    return true;
                }
                else
                {
                    return OnError();
                }
            });
        }

        private bool OnError()
        {
            throw new NotImplementedException();
        }
    }
}