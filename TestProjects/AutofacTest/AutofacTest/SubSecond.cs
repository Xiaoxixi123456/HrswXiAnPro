using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacTest
{
    public class SubSecond : ITest
    {
        private ICMM _cmm;
        public SubSecond(ICMM cmm)
        {
            _cmm = cmm;
        }

        public void Show()
        {
            _cmm.Func();
        }
    }
}
