using CalypsoServices;
using FileServices;
using Hrsw.XiAnPro.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoServerMain
{
    public class ViewModel : BindableBase
    {
        private ServiceHost _host;
        private ServiceHost _fileServiceHost;

        [Bindable]
        public string StatusMessage { get; set; }

        public DelegateCommand DirsSetupCommand { get; set; }
        public DelegateCommand LogsCommand { get; set; }


        public ViewModel()
        {
            _host = new ServiceHost(typeof(CalypsoService));
            _fileServiceHost = new ServiceHost(typeof(FileService));

            DirsSetupCommand = new DelegateCommand(DirsSetup);
            LogsCommand = new DelegateCommand(ShowLogs);

            
        }

        private void ShowLogs()
        {
            throw new NotImplementedException();
        }

        private void DirsSetup()
        {
            CalDirsSetupWindow dsWnd = new CalDirsSetupWindow();
            dsWnd.Topmost = true;
            dsWnd.DataContext = new DirsViewModel();
            dsWnd.ShowDialog();
        }

        public bool StartServices()
        {
            try
            {
                StartCalypsoService();
            }
            catch (Exception ex)
            {
                ServerLog.Logs.AddLog(ex.Message);
                return false;
            }
            try
            {
                StartFileService();
            }
            catch (Exception ex)
            {
                CloseCalypsoService();
                ServerLog.Logs.AddLog(ex.Message);
                return false;
            }
            StatusMessage = "服务启动成功";
            return true;
        }

        public void CloseServices()
        {
            CloseCalypsoService();
            CloseFileService();
        }

        private void StartCalypsoService()
        {
            _host.Opened += _host_Opened;
            _host.Closed += _host_Closed;
            _host.Faulted += _host_Faulted;
            _host.Open();
        }

        private void _host_Faulted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _host_Closed(object sender, EventArgs e)
        {
            _host.Opened -= _host_Opened;
            _host.Closed -= _host_Closed;
            _host.Faulted -= _host_Faulted;
        }

        private void _host_Opened(object sender, EventArgs e)
        {
           
        }

        private void StartFileService()
        {
            _fileServiceHost.Opened += _fileServiceHost_Opened;
            _fileServiceHost.Closed += _fileServiceHost_Closed;
            _fileServiceHost.Faulted += _fileServiceHost_Faulted;
            _fileServiceHost.Open();
        }

        private void _fileServiceHost_Faulted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _fileServiceHost_Closed(object sender, EventArgs e)
        {
            _fileServiceHost.Opened -= _fileServiceHost_Opened;
            _fileServiceHost.Closed -= _fileServiceHost_Closed;
            _fileServiceHost.Faulted -= _fileServiceHost_Faulted;
        }

        private void _fileServiceHost_Opened(object sender, EventArgs e)
        {
            
        }

        private void CloseCalypsoService()
        {
            if(_host.State == CommunicationState.Opened)
            {
                _host.Close();
            }
            else if (_host.State == CommunicationState.Closed)
            {
                _host.Abort();
            }
        }

        private void CloseFileService()
        {
            if (_fileServiceHost.State == CommunicationState.Opened)
            {
                _fileServiceHost.Close();
            }
            else if (_fileServiceHost.State == CommunicationState.Closed)
            {
                _fileServiceHost.Abort();
            }
        }
    }
}
