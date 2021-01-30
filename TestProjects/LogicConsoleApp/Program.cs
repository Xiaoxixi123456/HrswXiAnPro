using LogicTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LogicCtrl lc = new LogicCtrl();
            lc.Initial();

            Task.Run(() => lc.Start());
            char a = (char)Console.Read();
            if (a == 'q')
            {
                lc.Stop();
            }
            Console.ReadLine();
        }
    }
}
