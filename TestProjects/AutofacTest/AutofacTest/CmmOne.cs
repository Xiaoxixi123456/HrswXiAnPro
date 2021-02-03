using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacTest
{
    public class CmmOne : ICMM
    {
        private static int i = 1;
        public void Func()
        {
            Console.WriteLine("CmmOne " + i++);
        }
    }
}
