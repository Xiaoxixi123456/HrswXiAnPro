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
            _controlEvent = new AutoResetEvent(false);
            _selector = new PartSelectActivity();
            _mesPartActivity = new MeasurePartActivity(cmmControl);
            _controlFlag = null;
        }

        public async Task<bool> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            bool result = true;
            Part part = null;
            _controlFlag = true;
            while (true)
            {
                if (cts.IsCancellationRequested || !_controlFlag.HasValue)
                    break;
                if (_controlFlag.HasValue && _controlFlag.Value)
                {
                    part = await _selector.ExecuteAsync(tray, cts);
                }
                if (part == null 
                    || cts.IsCancellationRequested 
                    || !_controlFlag.HasValue)
                    break;
                result = await _mesPartActivity.ExecuteAsync(part, cts).ConfigureAwait(false);
                // TODO false 表示测量失败，这里需要停止等待人位选择
                if (!result)
                {
                    _controlEvent.Reset();
                    _controlEvent.WaitOne();
                }
            }
            return result;
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
