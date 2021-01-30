using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleAopTest
{
    public class MyTestCls
    {
        public virtual int TestMethod()
        {
            Console.WriteLine("Method..");
            Random rand = new Random(DateTime.Now.Second);
            int x = rand.Next(2);
            return x;
        }
    }

    public class MyInterceptorAspect : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("【DynamicProxy拦截器】");
            invocation.Proceed();
        }
    }
}
