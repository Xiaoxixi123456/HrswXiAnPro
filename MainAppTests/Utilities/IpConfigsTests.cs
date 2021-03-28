using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Utilities.Tests
{
    [TestClass()]
    public class IpConfigsTests
    {
        [TestMethod()]
        public void SetupIpConfigsTest()
        {
            IpConfigs.Inst.SetupIpConfigs();
        }
    }
}