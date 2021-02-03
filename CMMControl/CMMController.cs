using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Threading.Tasks;
using System.Threading;
using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.PCDmisImplement;
using System.Windows;

namespace Hrsw.XiAnPro.CMMControl
{
    public class CMMController : IAActivity<Part>, IDisposable
    {
        private AutoResetEvent _executedEvent;
        private PCDmisService _pcdmisService;
        private bool _executeSuccess;

        public CMMController()
        {
            _executedEvent = new AutoResetEvent(false);
            _pcdmisService = new PCDmisService();
            try
            {
                _pcdmisService.InitPcdmis();
                _pcdmisService.Executed += _pcdmisService_Executed;
                _pcdmisService.ExecuteError += _pcdmisService_ExecuteError;
                _pcdmisService.PcdmisMessageBox += _pcdmisService_PcdmisMessageBox;
            }
            catch (Exception)
            {
                // LOG记录
            }
        }

        private void _pcdmisService_PcdmisMessageBox(object sender, PcdmisEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void _pcdmisService_ExecuteError(object sender, PcdmisEventArgs e)
        {
            MessageBox.Show("PCDMIS执行出错。");
            _executeSuccess = false;
            _executedEvent.Set();
        }

        private void _pcdmisService_Executed(object sender, PcdmisEventArgs e)
        {
            if (!e.IsCancelled)
                _executeSuccess = true;
            else
                _executeSuccess = false;
            _executedEvent.Set();
        }

        public Task<bool> ExecuteAsync(Part obj, CancellationTokenSource cts)
        {
            //_executedEvent = new AutoResetEvent(false);
            _executedEvent.Reset();
            bool success = false;
            string prog = @"E:\WorkDirs\PcdmisFiles\SafePlane4.PRG";
            success = _pcdmisService.OpenProgram(prog);
            if (!success)
                return Task.FromResult(success);
            success = _pcdmisService.ExecuteProgramAsync();
            if (!success)
                return Task.FromResult(success);
            success = _executedEvent.WaitOne(TimeSpan.FromMinutes(10));
            if (!success)
                return Task.FromResult(success);
            return Task.FromResult(_executeSuccess);
        }

        public void Dispose()
        {
            if (_pcdmisService != null)
            {
                _pcdmisService.Executed -= _pcdmisService_Executed;
                _pcdmisService.ExecuteError -= _pcdmisService_ExecuteError;
                _pcdmisService.PcdmisMessageBox -= _pcdmisService_PcdmisMessageBox;
                _pcdmisService.Close();
            }
        }
    }
}
