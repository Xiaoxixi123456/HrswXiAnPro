﻿using Hrsw.XiAnPro.CMMClients;
using Hrsw.XiAnPro.CMMClients.PcdmisServiceReference;
using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.CMMClient
{
    public class PcdmisClient : BindableBase, ICMMControl, IDisposable
    {
        private static PcdmisClient _inst = new PcdmisClient();
        public static PcdmisClient Inst => _inst ?? (_inst = new PcdmisClient());
        private PCDmisServiceClient _pcdmisService;
        private PcdmisCallback _pcdmisCallback;

        private PcdmisClient() { CmmError = false; }
        private Timer _timer;

        public event EventHandler OfflineEvent;
        public event EventHandler OnlineEvent;

        [Bindable]
        public bool Connected { get; set; }
        [Bindable]
        public bool CmmError { get; set; }
        [Bindable]
        public string StatusMessage { get; set; }

        private ReportFileTransfer _reportFileTransfer;

        public bool Initial()
        {
            bool result = true;
            //连接服务器端
            try
            {
                OpenPcdmisService();
                OpenTransferReportsService();
            }
            catch (Exception ex)
            {
                result = false;
                Connected = false;
            }
            return result;
        }

        private void OpenPcdmisService()
        {
            _pcdmisCallback = new PcdmisCallback(this);
            _pcdmisService = new PCDmisServiceClient(new InstanceContext(_pcdmisCallback));
            _pcdmisService.InnerDuplexChannel.Faulted += InnerDuplexChannel_Faulted;
            _pcdmisService.InnerDuplexChannel.Opened += InnerDuplexChannel_Opened;
            _pcdmisService.InnerDuplexChannel.Closed += InnerDuplexChannel_Closed;
            _pcdmisService.Open();
            Connected = true;
        }

        private void OpenTransferReportsService()
        {
            string root = ClientDirsManager.Inst.PcdmisReportsDirectory;
            _reportFileTransfer = new ReportFileTransfer("Pcdmis");
            _reportFileTransfer.Initialize("PcdmisFileServiceEp");
            _reportFileTransfer.LaunchTransferProcess();
        }

        private void InnerDuplexChannel_Closed(object sender, EventArgs e)
        {
            try
            {
                //if (_pcdmisService.State == CommunicationState.Opened)
                //{
                //    _pcdmisService.Close();
                //}
                //else if (_pcdmisService.State == CommunicationState.Faulted)
                //{
                //    _pcdmisService.Abort();
                //}
                _pcdmisService.InnerDuplexChannel.Faulted -= InnerDuplexChannel_Faulted;
                _pcdmisService.InnerDuplexChannel.Opened -= InnerDuplexChannel_Opened;
                _pcdmisService.InnerDuplexChannel.Closed -= InnerDuplexChannel_Closed;
                _pcdmisService = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Connected = false;
        }

        public void CloseServics()
        {
            ClosePcdmisService();
            CloseFileTransferService();
        }

        private void CloseFileTransferService()
        {
            try
            {
                if (_reportFileTransfer != null)
                {
                    _reportFileTransfer.StopStopTransferProcess();
                    _reportFileTransfer.StopFileTransferService();
                }
            }
            catch (Exception)
            {
            }
        }

        public void ClosePcdmisService()
        {
            try
            {
                if (_pcdmisService != null)
                {
                    if (_pcdmisService.State == CommunicationState.Faulted)
                    {
                        _pcdmisService.Abort();
                    }
                    else if (_pcdmisService.State == CommunicationState.Opened)
                    {
                        _pcdmisService.Disconnect();
                        _pcdmisService.Close();
                    }
                }
            }
            catch (Exception)
            {
                // TODO 关闭通讯失败
                _pcdmisService.Abort();
            }
            Connected = false;
        }

        private void InnerDuplexChannel_Opened(object sender, EventArgs e)
        {
            Debug.WriteLine("channel opened..");
            var response = _pcdmisService.Connect();
            if (!response.Success)
            { }
        }

        private void InnerDuplexChannel_Faulted(object sender, EventArgs e)
        {
            // TODO 服务器断开提醒
            _pcdmisService.Abort();
        }



        public Task<bool> AnalyseReportAsync(out bool pass)
        {
            pass = false;
            return Task.FromResult(true);
        }

        public Task<bool> GotoSafePositionAsync()
        {
            return Task.FromResult(true);
        }

        public async Task<bool> MeasurePartAsync(Part part)
        {
            //判断结果，并启动文件回传
            bool success = true;
            try
            {
                part.Status = PartStatus.PS_Measuring;
                part.Flag = 0;
                _timer = new Timer(TestBack/*_ => part.Flag = ++part.Flag % 2*/, part, 0, 2000);

                PCDResponse response = await Task.Run(() => Measure(part));

                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                part.Flag = 0;
                success = response.Success;
                part.Pass = response.Pass;
                part.ResultFile = response.ReportFile;
                part.Status = success ? PartStatus.PS_Measured : PartStatus.PS_Error;
            }
            catch (Exception) 
            {
                //通讯失败中终止测量
                success = false;
                part.Pass = false;
                OfflineEvent?.Invoke(this, null);
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                part.Flag = 0;
                part.ResultFile = "";
                part.Status = PartStatus.PS_Idle;
                throw new InvalidOperationException("远程测量异常");
            }

            return success;
        }

        private void TestBack(object obj)
        {
            Part part = (Part)obj;
            part.Flag = ++part.Flag % 2;
        }

        // 当选择流程分支时，释放测量
        public bool ReleaseMeasure()
        {
            try
            {
                _pcdmisService.ReleaseMeasure();
                return true;
            }
            catch (Exception)
            {
                OfflineEvent?.Invoke(this, null);
                return false;
            }
        }

        private PCDResponse Measure(Part part)
        {
            PCDRequest request = new PCDRequest() { Part = part };
            PCDResponse response = _pcdmisService.MeasurePart(request);
            return response;
        }

        public void PauseMeasure()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            CloseServics();
        }

        public bool Offline()
        {
            CloseServics();
            return Connected;
        }

        public bool Online()
        {
            if (!Connected)
            {
                return Initial();
            }
            else
            {
                return true;
            }
        }

        public void TransferReport(Part obj)
        {
            _reportFileTransfer.Next(obj);
        }
    }
}
