using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class MeasurePartActivity : IAActivity<Part, bool>
    {
        private ICMMControl _cmm;

        public MeasurePartActivity(ICMMControl cmm)
        {
            _cmm = cmm;
        }

        public MeasurePartActivity()
        {
            //_cmm = new CMMController();
        }

        public async Task<bool> ExecuteAsync(Part obj, CancellationTokenSource cts)
        {
            
            bool success = await _cmm.MeasurePartAsync(obj);
            if (success)
            {
                // 测量完成启动文件传输
                // 往等待队列中发送Part
                _cmm.TransferReport(obj);
            }
            return success;
        }

        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Retry()
        {
            throw new NotImplementedException();
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }
    }
}
