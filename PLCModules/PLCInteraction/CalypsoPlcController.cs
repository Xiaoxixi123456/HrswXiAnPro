using Hrsw.XiAnPro.Models;
using PLCServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PLCInteraction
{
    public class CalypsoPlcController
    {
        //private static CalypsoPlcController _inst = new CalypsoPlcController();
        //public static CalypsoPlcController Inst => _inst ?? (_inst = new CalypsoPlcController());
        private PLCAccessor _plcAccessor;
        private CancellationTokenSource _cts;
        private int _dbNumber;
        public event EventHandler ErrorEvent;
        private bool _error;

        public CalypsoPlcController()
        {
            _plcAccessor = PLCAccessor.Instance;
            _dbNumber = 2;
        }

        public async Task<bool> MeasurePartAsync(Part part)
        {
            _error = false;
            try
            {
                StartMonitor();
                StartMeasure(part);
                await WaitMeasureEnd(part).ConfigureAwait(false);
                EndMonitor();
            }
            catch (Exception)
            {
                EndMonitor();
                _error = true;
            }
            return _error ? false : true;
        }

        private void StartMeasure(Part part)
        {
            // TODO 传递表头过程
            _plcAccessor.WriteMasks(_dbNumber, 0, 0x10);
        }


        private Task WaitMeasureEnd(Part part)
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    // TODO 出现错误是否跳出

                    bool completed = OnCompleted(part);
                    if (completed) break;

                    Task.Delay(1000).Wait();
                }
            });
        }

        private void StartMonitor()
        {
            _cts = new CancellationTokenSource();
            // 
            Task.Run(() =>
            {
                while (true)
                {
                    if (_cts.IsCancellationRequested) break;

                    try
                    {
                        OnError();
                    }
                    catch (Exception ex)
                    {
                        // 错误监控时抛出异常
                    }

                    Task.Delay(1000).Wait();
                }
            }, _cts.Token);
        }

        private void EndMonitor()
        {
            _cts?.Cancel();
        }

        private void OnError()
        {
            _plcAccessor.ReadMask(_dbNumber, 0, 6, out _error);
            if (_error)
            {
                ErrorEvent?.BeginInvoke(this, null, null, null);
            }
        }

        private bool OnCompleted(Part part)
        {
            bool result;
            // 读取测量完成标志
            _plcAccessor.ReadMask(_dbNumber, 0, 5, out result);
            if (result)
            {
                // 测量完成标志清零
                _plcAccessor.WriteMasks(_dbNumber, 0, false, 5);
                bool pass;
                _plcAccessor.ReadMask(_dbNumber, 0, 7, out pass);
                part.Pass = pass;
            }
            return result;
        }
    }
}
