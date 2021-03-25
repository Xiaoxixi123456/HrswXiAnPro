using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PLCInteraction
{
    public class UnloadControl : PlcControlBase
    {
        public UnloadControl() : base()
        {

        }

        public override void Startup(Tray tray)
        {
            _plcAccessor.WriteMasks(_dbNumber, 0, 0x04);
        }

        public override bool OnCompleted()
        {
            bool result;
            // 读取下料完成标志
            _plcAccessor.ReadMask(_dbNumber, 0, 3, out result);
            if (result)
            {
                // 下料料完成标志清零
                _plcAccessor.WriteMasks(_dbNumber, 0, false, 3);
            }
            return result;
        }

        public override void OnError()
        {
        }
    }
}
