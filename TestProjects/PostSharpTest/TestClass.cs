using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharpTest
{
    public class TestClass
    {
        [LogAspectTest]
        public void Method1()
        {
            Console.WriteLine("Method1");
        }
    }
}
