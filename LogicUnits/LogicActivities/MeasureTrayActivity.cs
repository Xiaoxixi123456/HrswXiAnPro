using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using System.Threading;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class MeasureTrayActivity : IAActivity<Tray, bool>
    {
        private IAActivity<Tray, Part> _selector;
        private IAActivity<Part, bool> _mesPartActivity;
        private AutoResetEvent _controlEvent;
        /// <summary>
        /// 控制标志，null退出，false重试，true下一个
        /// </summary>
        private bool? _controlFlag;
        private bool _commuFault;
        
        public MeasureTrayActivity(ICMMControl cmmControl)
        {
            _controlFlag = null;
            _commuFault = false;
            _controlEvent = new AutoResetEvent(false);
            _selector = new PartSelectActivity();
            _mesPartActivity = new MeasurePartActivity(cmmControl);
        }

        public async Task<bool> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            Part part = null;
            _controlFlag = true;
            _commuFault = false;
            tray.Status = TrayStatus.TS_Measuring;
            while (true)
            {
                if (cts.IsCancellationRequested)
                {
                    return false;
                }
                else if (!_controlFlag.HasValue)
                {
                    break;
                }

                if (_controlFlag.Value)
                {
                    part = await _selector.ExecuteAsync(tray, cts);
                }

                if (cts.IsCancellationRequested)
                {
                    return false;
                }
                if (part == null)
                {
                    break;
                }

                SetupPartParams(tray, part);

                //try // TODO 与服务器连接断开，测量抛出无效操作异常
                //{
                    bool success = await _mesPartActivity.ExecuteAsync(part, cts).ConfigureAwait(false);

                    if (!success)
                    {
                        _controlEvent.Reset();
                        _controlEvent.WaitOne();
                    }
                //}
                //catch (Exception)
                //{
                //    throw new InvalidOperationException();
                //}
            }
            tray.Status = _commuFault ? TrayStatus.TS_Error : TrayStatus.TS_Measured;
            return !_commuFault;
        }

        private void SetupPartParams(Tray tray, Part part)
        {
            part.UseCmmNo = tray.UseCmmNo;
            CalcPartOffset(tray, part);
        }

        public void CalcPartOffset(Tray tray, Part part)
        {
            // 第一个工件基准坐标
            double yOrgBas = tray.BaseRowOffset;
            double xOrgBas = tray.BaseColumnOffset;

            int nb = part.SlotNb;
            int rnb = (nb - 1) / tray.ColumnCount;
            int cnb = (nb - 1) % tray.ColumnCount;

            double xBas = xOrgBas + cnb * tray.ColumnOffset;
            double yBas = yOrgBas - rnb * tray.RowOffset;

            part.XOffset = xBas;
            part.YOffset = yBas;
        }

        public void Next()
        {
            _controlFlag = true;
            _controlEvent.Set();
        }

        public void Retry()
        {
            _controlFlag = false;
            _controlEvent.Set();
        }

        public void Complete()
        {
            _controlFlag = null;
            _controlEvent.Set();
        }
    }
}
