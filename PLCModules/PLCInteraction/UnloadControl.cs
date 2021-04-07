using ClientCommonMods;
using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Hrsw.XiAnPro.PLCInteraction
{
    public class UnloadControl : PlcControlBase
    {
        public UnloadControl() : base()
        {

        }

        public override Task Startup(Tray tray)
        {
            return Task.Run(() =>
            {
                MyEventAggregator.Inst.GetEvent<UnloadEvent>().Publish(true);
                _plcAccessor.PlcButton(3, 32, 3, true, 0.3);
                _plcAccessor.WriteSInt(_dbNumber, 2, tray.SlotNb/* - 1*/);
                _plcAccessor.WriteMasks(_dbNumber, 0, 0x04);
                _plcAccessor.PlcButton(3, 32, 0, true, 0.3);
            });
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
                _plcAccessor.WriteSInt(_dbNumber, 2, 0);
                MyEventAggregator.Inst.GetEvent<UnloadEvent>().Publish(false);
            }
            return result;
        }

        public override void OnError()
        {
            bool result;
            _plcAccessor.ReadMask(3, 32, 2, out result);
            if (result)
            {
                MyEventAggregator.Inst.GetEvent<PlcErrorEvent>().Publish(new PlcErrorStatus() {  Error = true });
                var hl = MessageBox.Show("机器人下料过程中错误", "错误", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk, MessageBoxResult.Cancel, MessageBoxOptions.DefaultDesktopOnly);
                if (hl == MessageBoxResult.OK)
                {
                    _plcAccessor.PlcButton(3, 32, 3, true, 0.3);
                    MyEventAggregator.Inst.GetEvent<PlcErrorEvent>().Publish(new PlcErrorStatus() {  Error = false });
                }
                else
                {
                    _error = true;
                }
            }
        }
    }
}
