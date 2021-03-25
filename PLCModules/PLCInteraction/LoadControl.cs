using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;

namespace Hrsw.XiAnPro.PLCInteraction
{
    public class LoadControl : PlcControlBase
    {
        public LoadControl() : base()
        {

        }

        public override void Startup(Tray tray)
        {
            _plcAccessor.WriteInt(_dbNumber, 2, tray.SlotNb - 1);
            _plcAccessor.WriteMasks(_dbNumber, 0, 0x01);
        }

        public override bool OnCompleted()
        {
            bool result;
            // 读取上料完成标志
            _plcAccessor.ReadMask(_dbNumber, 0, 1, out result);
            if (result)
            {
                // 上料完成标志清零
                _plcAccessor.WriteMasks(_dbNumber, 0, false, 1);
            }
            return result;
        }

        public override void OnError()
        {
        }
    }
}
