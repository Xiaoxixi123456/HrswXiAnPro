using Hrsw.XiAnPro.Models;
using PLCServices;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PLCInteraction
{
    public class LoadControl1
    {
        private PLCAccessor _plcAccessor;
        private int _dbNumber;
        private bool _completed;

        public LoadControl1()
        {
            _plcAccessor = PLCAccessor.Instance;
        }

        public async Task<bool> ExecuteAsync(Tray tray)
        {
            _dbNumber = tray.UseCmmNo;
            try
            {
                Startup();
                _completed = await OnMonitor().ConfigureAwait(false);
            }
            catch (InvalidOperationException ex)
            {
                // TODO 记录无效操作
                _completed = false;
            }
            return _completed;
        }

        private Task<bool> OnMonitor()
        {
            return Task.Run(() =>
            {
                bool result;
                while (true)
                {
                    result = OnCompleted();
                    if (result)
                        break;
                    result = OnError();
                    if (!result)
                        break;
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                return result;
            });
        }

        private bool OnCompleted()
        {
            bool result;
            _plcAccessor.ReadMask(_dbNumber, 0, 1, out result);
            if (result)
                _plcAccessor.WriteMasks(_dbNumber, 0, true, 1);
            return result;
        }

        private bool OnError()
        {
            bool result;
            bool retry = false;
            // TODO LoadActivity暂停不可继续
            // 返回两种可能
            _plcAccessor.ReadMask(_dbNumber, 0, 2, out result);
            // 如果设置了错误标志，提示人工选择处理方式
            if (result)
            {
                retry = true;
            }
            return retry;
        }

        private void Startup()
        {
            _plcAccessor.WriteMasks(_dbNumber, 0, 0x01);
        }
    }
}