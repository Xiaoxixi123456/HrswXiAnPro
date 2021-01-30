using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharpTest
{
    [Serializable]
    public class LogAspectTest : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            //base.OnInvoke(args);
            args.Proceed();
            Console.WriteLine("5.0.44");
        }
    }
}
