using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacTest
{
    public class SubFirst : ITest
    {
        private ITest _sub;
        public SubFirst(ITest sub)
        {
            _sub = sub;
        }

        public void Show()
        {
            _sub.Show();
        }
    }
}
