using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLCServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            plcAccessor = PLCAccessor.Instance;
            plcAccessor.PlcIP = "192.168.18.18";
        }

        [TestMethod()]
        public void ConnectTest()
        {
            plcAccessor = PLCAccessor.Instance;
            plcAccessor.PlcIP = "192.168.18.16";
            /*int success = */plcAccessor.Connect();
        }

        [TestMethod()]
        public void DisconnectTest()
        {
            plcAccessor.Disconnect();
        }

        [TestMethod()]
        public void WriteMasksTest()
        {
            plcAccessor = PLCAccessor.Instance;
            plcAccessor.PlcIP = "192.168.18.18";
            bool ok = false;
            string message = "";
            try
            {
                plcAccessor.Connect();
                plcAccessor.WriteMasks(1, 258, 0x0F);
                plcAccessor.WriteMasks(1, 258, false, 0, 1, 2, 3, 4, 5, 6, 7);
                plcAccessor.WriteMasks(1, 258, true, 1, 3);
                plcAccessor.WriteString(1, 0, 256, "abcdesffgassd");
                //plcAccessor.WriteASCIIString(1, 258, "ab");
                plcAccessor.ReadASCIIString(1, 258, 2, out message);
                plcAccessor.ReadString(1, 0, 256, out message);
                plcAccessor.ReadMask(1, 258, 3, out ok);
                plcAccessor.Disconnect();
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.Message);
            }
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