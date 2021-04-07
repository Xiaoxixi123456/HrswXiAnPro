using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using System.Threading;
using System.Windows;
using ClientCommonMods;

namespace Hrsw.XiAnPro.PLCInteraction
{
    public class LoadControl : PlcControlBase
    {
        public LoadControl() : base()
        {

        }

        public override Task Startup(Tray tray)
        {
            return Task.Run(() =>
            {
                //_plcAccessor.WriteMasks(3, 32, true, 3);
                //Thread.Sleep(500);
                //_plcAccessor.WriteMasks(3, 32, false, 3);
                MyEventAggregator.Inst.GetEvent<LoadEvent>().Publish(true);
                _plcAccessor.PlcButton(3, 32, 3, true, 0.3);
                _plcAccessor.WriteSInt(_dbNumber, 2, tray.SlotNb/* - 1*/);
                _plcAccessor.WriteMasks(_dbNumber, 0, 0x01);
                //_plcAccessor.WriteMasks(3, 32, true, 0);
                //Thread.Sleep(500);
                //_plcAccessor.WriteMasks(3, 32, false, 0);
                _plcAccessor.PlcButton(3, 32, 0, true, 0.3);
            });
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
                _plcAccessor.WriteSInt(_dbNumber, 2, 0);
                MyEventAggregator.Inst.GetEvent<LoadEvent>().Publish(false);
            }
            return result;
        }

        public override void OnError()
        {
            bool result;
            _plcAccessor.ReadMask(3, 32, 2, out result);
            if (result)
            {
                MyEventAggregator.Inst.GetEvent<PlcErrorEvent>().Publish(new PlcErrorStatus() { Error = true });
                var hl = MessageBox.Show("机器人上料过程中错误", "错误", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk, MessageBoxResult.Cancel, MessageBoxOptions.DefaultDesktopOnly);
                if (hl == MessageBoxResult.OK)
                {
                    // 错误复位
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
