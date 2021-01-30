using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLCServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCServices.Tests
{
    [TestClass()]
    public class PLCAccessorTests
    {
        PLCAccessor plcAccessor;
        [TestMethod()]
        public void PLCAccessorTest()
        {
            plcAccessor = new PLCAccessor();
            plcAccessor.PlcIP = "192.168.18.18";
        }

        [TestMethod()]
        public void ConnectTest()
        {
            plcAccessor = new PLCAccessor();
            plcAccessor.PlcIP = "192.168.18.18";
            int success = plcAccessor.Connect();
        }

        [TestMethod()]
        public void DisconnectTest()
        {
            int success = plcAccessor.Disconnect();
        }

        [TestMethod()]
        public void WriteMasksTest()
        {
            plcAccessor = new PLCAccessor();
            plcAccessor.PlcIP = "192.168.18.18";
            int success = plcAccessor.Connect();
            success = plcAccessor.WriteMasks(1, 258, 0x0F);
            success = plcAccessor.WriteMasks(1, 258, false, 0,1,2,3,4,5,6,7);
            success = plcAccessor.WriteMasks(1, 258, true, 1, 3);
            success = plcAccessor.WriteString(1, 0, 256, "abcdesffgassd");
            //success = plcAccessor.WriteASCIIString(1, 258, "ab");
            string message = "";
            success = plcAccessor.ReadASCIIString(1, 258, 2, out message);
            success = plcAccessor.ReadString(1, 0, 256, out message);
            bool ok;
            success = plcAccessor.ReadMask(1, 258, 3, out ok);
            plcAccessor.Disconnect();
            Assert.AreEqual(ok, true);
        }

        [TestMethod()]
        public void WriteMasksTest1()
        {

        }

        [TestMethod()]
        public void ReadMaskTest()
        {

        }

        [TestMethod()]
        public void WriteStringTest()
        {

        }

        [TestMethod()]
        public void WriteASCIIStringTest()
        {

        }

        [TestMethod()]
        public void ReadStringTest()
        {

        }

        [TestMethod()]
        public void ReadASCIIStringTest()
        {

        }
    }
}