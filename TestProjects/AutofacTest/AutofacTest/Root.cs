using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacTest
{
    public class Root : ITest
    {
        private ITest _sub;
        public Root(ITest sub)
        {
            _sub = sub;
        }

        public void Show()
        {
            _sub.Show();
        }
    }
}
