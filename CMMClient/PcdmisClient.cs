﻿using Hrsw.XiAnPro.CMMClients.PcdmisServiceReference;
using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.CMMClient
{
    public class PcdmisClient : BindableBase, ICMMControl
    {
        private static PcdmisClient _inst = new PcdmisClient();
        public static PcdmisClient Inst => _inst ?? (_inst = new PcdmisClient());
        private PCDmisServiceClient _pcdmisService;
        private PcdmisCallback _pcdmisCallback;

        private PcdmisClient() { }
        private Timer _timer;

        [Bindable]
        public bool MeasError { get; set; }

        public bool Initial()
        {
            bool result = false;
            //连接服务器端
            _pcdmisCallback = new PcdmisCallback();
            _pcdmisService = new PCDmisServiceClient(new InstanceContext(_pcdmisCallback));
            _pcdmisService.InnerDuplexChannel.Faulted += InnerDuplexChannel_Faulted;
            _pcdmisService.InnerDuplexChannel.Opened += InnerDuplexChannel_Opened;
            _pcdmisService.Open();
            return result;
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
            _pcdmisService.Abort();
        }

        public void Close()
        {
            if (_pcdmisService != null)
            {
                if (_pcdmisService.State == CommunicationState.Faulted)
                    _pcdmisService.Abort();
                else if (_pcdmisService.State == CommunicationState.Opened)
                {
                    _pcdmisService.Disconnect();
                    _pcdmisService.Close();
                }
            }
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
            // TODO 判断结果，并启动文件回传
            bool success = true;
            try
            {
                part.Status = PartStatus.PS_Measuring;
                part.Flag = 1;
                _timer = new Timer(_ => ++part.Flag, part, 0, 2000);

                PCDResponse response = await Task.Run(() => Measure(part));

                _timer.Change(Timeout.Infinite, Timeout.Infinite);

                success = response.Success;
                part.Pass = response.Pass;
                part.Status = success ? PartStatus.PS_Measured : PartStatus.PS_Error;

            }
            catch (Exception) 
            {
                //通讯失败中终止测量
                success = false;
                part.Pass = false;
                part.Status = PartStatus.PS_Idle;
                throw new InvalidOperationException("远程测量异常");
            }

            return success;
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
            throw new NotImplementedException();
        }
    }
}
