using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hrsw.XiAnPro.PCDmisService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PCDmisService.Tests
{
    [TestClass()]
    public class MeasProgManagerTests
    {
        [TestMethod()]
        public void LoadProgramsTest()
        {
            MeasProgManager.Inst.SavFileName = "measprogs.xml";
            MeasProgManager.Inst.LoadPrograms();
            Assert.AreEqual(MeasProgManager.Inst.MeasProgs.Count, 20);
        }

        [TestMethod()]
        public void SaveProgramsTest()
        {
            MeasProgManager.Inst.SavFileName = "measprogs.xml";
            for (int i = 0; i < 20; i++)
            {
                MeasProgManager.Inst.MeasProgs.Add(new MeasProg() { Id = i, FileName = $"{i}.prg" });
            }
            MeasProgManager.Inst.SavePrograms();
        }

        [TestMethod()]
        public void GetMeasProgTest()
        {
            MeasProgManager.Inst.SavFileName = "measprogs.xml";
            MeasProgManager.Inst.LoadPrograms();
            string ss = MeasProgManager.Inst.GetMeasProg(2);
            Assert.AreEqual(ss, "2.prg");
            ss = MeasProgManager.Inst.GetMeasProg(25);
            Assert.AreEqual(ss, string.Empty);

            ss = MeasProgManager.Inst.GetMeasProg(3);
            Assert.AreEqual(ss, string.Empty);
        }

        [TestMethod()]
        public void AddMeasProgTest()
        {
            LoadProgramsTest();
            MeasProgManager.Inst.AddMeasProg(3, "3.prg");
            int cm = MeasProgManager.Inst.MeasProgs.Count;
            Assert.AreEqual(cm, 20);

            MeasProgManager.Inst.AddMeasProg(21, "21.prg");
            cm = MeasProgManager.Inst.MeasProgs.Count;
            Assert.AreEqual(cm, 21);
        }

        [TestMethod()]
        public void DeleteMeasProgTest()
        {
            MeasProgManager.Inst.LoadPrograms();
            MeasProgManager.Inst.DeleteMeasProg(3);
            int cm = MeasProgManager.Inst.MeasProgs.Count;
            Assert.AreEqual(cm, 19);
            MeasProgManager.Inst.SavePrograms();
        }
    }
}