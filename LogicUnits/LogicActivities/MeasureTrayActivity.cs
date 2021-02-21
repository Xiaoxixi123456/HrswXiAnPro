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
        
        public MeasureTrayActivity(ICMMControl cmmControl)
        {
            _controlFlag = null;
            _controlEvent = new AutoResetEvent(false);
            _selector = new PartSelectActivity();
            _mesPartActivity = new MeasurePartActivity(cmmControl);
        }

        public async Task<bool> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            Part part = null;
            _controlFlag = true;
            while (true)
            {
                if (cts.IsCancellationRequested)
                    return false;
                else if (!_controlFlag.HasValue)
                {
                    break;
                }
                if (_controlFlag.HasValue && _controlFlag.Value)
                {
                    part = await _selector.ExecuteAsync(tray, cts);
                }
                if (cts.IsCancellationRequested)
                    return false;
                if (part == null || !_controlFlag.HasValue)
                    break;
               bool success = await _mesPartActivity.ExecuteAsync(part, cts).ConfigureAwait(false);
                if (!success)
                {
                    _controlEvent.Reset();
                    _controlEvent.WaitOne();
                }
            }
            return true;
        }

        public void Next()
        {
            _controlFlag = true;
            _controlEvent.WaitOne();
        }

        public void Retry()
        {
            _controlFlag = false;
            _controlEvent.WaitOne();
        }

        public void Complete()
        {
            _controlFlag = null;
            _controlEvent.WaitOne();
        }
    }
}
