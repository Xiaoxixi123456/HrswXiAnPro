using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using System.Threading;

namespace AutoFlowBindingTest
{
    public class Cmm : ICMM
    {
        AutoResetEvent evt = new AutoResetEvent(false);
        // PCDMIS 实现

        private bool _success;
        public void ClearError()
        {
            throw new NotImplementedException();
        }

        public bool Measure(Part part)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MeasureAsync(Part part)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MeasureAsync(Part part, CancellationTokenSource cts)
        {
            Task.Run(() =>
            {
                Doworking();
            });
            evt.WaitOne();
            return Task.FromResult<bool>(_success);
        }

        private void Doworking()
        {
            Thread.Sleep(5000); // 模拟PCDMIS实现
            _success = true;
            evt.Set();
        }

        public void Offline()
        {
            throw new NotImplementedException();
        }

        public void Online()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
