using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Func1();
            Console.ReadLine();
        }

        public static async void Func1()
        {
            Console.WriteLine("Func1 tid : " + Thread.CurrentThread.ManagedThreadId);
            await Func2();
        }

        private static async Task Func2()
        {
            Console.WriteLine("Func2 tid : " + Thread.CurrentThread.ManagedThreadId);
            await Func3();
        }

        private static async Task Func3()
        {
            Console.WriteLine("Func3 tid : " + Thread.CurrentThread.ManagedThreadId);
            await Task.Run(() => Console.WriteLine("tid : " + Thread.CurrentThread.ManagedThreadId));
        }
    }
}
