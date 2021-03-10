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
        private int _dbNumber;
        public LoadControl() : base()
        {

        }
        public override Task Startup(Tray tray)
        {
            return Task.Run(() => _plcAccessor.WriteMasks(_dbNumber, 258, 0x01));
        }

        public override void Initialize(Tray tray)
        {
            //_dbNumber = tray.UseCmmNo;
            // TODO 存储块号选择
            _dbNumber = tray.UseCmmNo == 0 ? 3 : 4;
        }

        public override bool OnCompleted()
        {
            bool result;
            _plcAccessor.ReadMask(_dbNumber, 258, 1, out result);
            if (result)
                _plcAccessor.WriteMasks(_dbNumber, 258, true, 1);
            return result;
        }

        public override bool OnError()
        {
            bool result;
            // LoadActivity暂停不可继续
            // 返回两种可能
            _plcAccessor.ReadMask(_dbNumber, 258, 2, out result);
            // 如果设置了错误标志，提示人工选择处理方式
            return !result;
        }
    }
}
