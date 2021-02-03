using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacTest
{
    public class CmmSecond : ICMM
    {
        private int i = 1;
        public void Func()
        {
            Console.WriteLine("CmmSecond" + i++);
        }
    }
}
