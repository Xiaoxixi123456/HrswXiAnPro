using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hrsw.XiAnPro.LogicActivities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;

namespace Hrsw.XiAnPro.LogicActivities.Tests
{
    [TestClass()]
    public class MeasureTrayActivityTests
    {
        [TestMethod()]
        public void CalcPartOffsetTest()
        {
            MeasureTrayActivity mta = new MeasureTrayActivity(null, null);
            Tray tray = new Tray()
            {
                BaseColumnOffset = -20,
                BaseRowOffset = 50,
                ColumnCount = 4,
                RowCount = 4,
                ColumnOffset = 10,
                RowOffset = 10
            };
            Part part1 = new Part()
            {
                SlotNb = 6
            };

            mta.CalcPartOffset(tray, part1);

            Assert.AreEqual(part1.XOffset, -10);
            Assert.AreEqual(part1.YOffset, 40);

            Part part2 = new Part()
            {
                SlotNb =  11
            };

            mta.CalcPartOffset(tray, part2);

            Assert.AreEqual(part2.XOffset, 0);
            Assert.AreEqual(part2.YOffset, 30);

            Part part3 = new Part()
            {
                SlotNb = 13
            };

            mta.CalcPartOffset(tray, part3);

            Assert.AreEqual(part3.XOffset, -20);
            Assert.AreEqual(part3.YOffset, 20);
        }
    }
}