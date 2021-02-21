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
        private int _dbNumber;
        public UnloadControl() : base()
        {

        }

        public override Task Startup(Tray tray)
        {
            return Task.Run(() => _plcAccessor.WriteMasks(_dbNumber, 258, 0x04));
        }

        public override void Setup(Tray tray)
        {
            _dbNumber = tray.UseCmmNo;
        }

        public override bool OnCompleted()
        {
            bool result;
            _plcAccessor.ReadMask(_dbNumber, 258, 4, out result);
            if (result)
                _plcAccessor.WriteMasks(_dbNumber, 258, false, 4);
            return result;
        }

        public override bool OnError()
        {
            bool result;
            bool retry = true;
            // TODO LoadActivity暂停不可继续
            // 返回两种可能
            _plcAccessor.ReadMask(_dbNumber, 258, 5, out result);
            // 如果设置了错误标志，提示人工选择处理方式
            if (result)
            {
                retry = false;
            }
            return retry;
        }
    }
}
