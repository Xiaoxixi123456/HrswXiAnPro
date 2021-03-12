﻿using Hrsw.XiAnPro.PCDmisService;
using Hrsw.XiAnPro.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PcdmisServerMain
{
    public class ViewModel : BindableBase, IDisposable
    {
        private ServiceHost _host;

        public PCDmisService PcdmisService { get; set; }
        //[Bindable]
        //public string StatusInfo { get; set; }
        [Bindable]
        public MeasProgManager MeasProgsManager { get; set; }
        [Bindable]
        public ServerDirManager ServerDirs { get; set; }

        public DelegateCommand ProgsSetupCommand { get; set; }
        public DelegateCommand DirsSetupCommand { get; set; }
        public DelegateCommand LogsCommand { get; set; }


        public ViewModel()
        {
            PcdmisService = new PCDmisService();
            MeasProgsManager = MeasProgManager.Inst;
            MeasProgsManager.SavFileName = "MeasProgs.xml";
            MeasProgsManager.LoadPrograms();
            ServerDirs = ServerDirManager.Inst;
            ServerDirManager.Inst.LoadDirs();

            ProgsSetupCommand = new DelegateCommand(ProgsSetup);
            DirsSetupCommand = new DelegateCommand(DirsSetup);
            LogsCommand = new DelegateCommand(ShowLogs);
        }

        private void ShowLogs()
        {
            LogsWindow lgWnd = new LogsWindow();
            LogsViewModel lgVm = new LogsViewModel();
            lgWnd.DataContext = lgVm;
            lgWnd.Topmost = true;
            lgWnd.ShowDialog();
        }

        private void DirsSetup()
        {
            DirsSetupWindow dsWnd = new DirsSetupWindow();
            dsWnd.Topmost = true;
            dsWnd.DataContext = new DirsSetupViewModel();
            dsWnd.ShowDialog();
        }

        private void ProgsSetup()
        {
            ProgramSetupWindow psWnd = new ProgramSetupWindow();
            psWnd.Topmost = true;
            ProgsSetupViewModel psVm = new ProgsSetupViewModel();
            psVm.MeasProgs = MeasProgsManager.MeasProgs;
            psWnd.DataContext = psVm;
            psWnd.ShowDialog();
        }

        public void Initialize()
        {
            PcdmisService.Initial();
            _host = new ServiceHost(PcdmisService);
            _host.Opened += _host_Opened;
            _host.Faulted += _host_Faulted;
            _host.Open();
        }

        private void _host_Faulted(object sender, EventArgs e)
        {
            PcdmisService.StatusMessage = "服务器错误";
            ServerLog.Logs.AddLog("服务器错误！");
        }

        private void _host_Opened(object sender, EventArgs e)
        {
            PcdmisService.StatusMessage = "服务已开启";
            ServerLog.Logs.AddLog("服务器开启！");
        }

        public void Dispose()
        {
            PcdmisService?.Dispose();
            MeasProgsManager.SavePrograms();
            ServerDirs.SaveDirs();
            if (_host == null)
                return;
            if (_host.State == CommunicationState.Opened)
            {
                _host.Close();
            }
            if (_host.State == CommunicationState.Faulted)
            {
                _host.Abort();
            }
        }
    }
}
