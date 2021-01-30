using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleAopTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxy = new ProxyGenerator();
            var mcls = proxy.CreateClassProxy<MyTestCls>(new MyInterceptorAspect());
            int i = mcls.TestMethod();

            Console.ReadLine();
        }
    }
}
