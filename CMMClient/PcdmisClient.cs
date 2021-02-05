using Hrsw.XiAnPro.CMMClients.PcdmisServiceReference;
using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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

        public bool Initial()
        {
            bool result = false;
            // TODO 连接服务器端
            _pcdmisCallback = new PcdmisCallback();
            _pcdmisService = new PCDmisServiceClient(new InstanceContext(_pcdmisCallback));
            _pcdmisService.InnerDuplexChannel.Faulted += InnerDuplexChannel_Faulted;
            _pcdmisService.InnerDuplexChannel.Opened += InnerDuplexChannel_Opened;
            _pcdmisService.Open();
            return result;
        }

        private void InnerDuplexChannel_Opened(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
                    _pcdmisService.Close();
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

        public Task<AActivityFlags> MeasurePartAsync(Part part)
        {
            AActivityFlags flag = new AActivityFlags() { Next = true };
            // TODO 判断结果，并启动文件回传

            try
            {
                part.Status = PartStatus.PS_Measuring;
                part.Flag = 1;
                _timer = new Timer(_ => ++part.Flag, part, 0, 2000);

                PCDResponse response = Measure(part);

                _timer.Change(Timeout.Infinite, Timeout.Infinite);

                part.Pass = response.Pass;
                if (response.Success)
                {
                    part.Status = PartStatus.PS_Measured;
                }
                else
                {
                    part.Status = PartStatus.PS_Error;
                }

                flag.Next = response.IsNext;
                AActivityFlags.IsExit = false;
            }
            catch (Exception e) 
            {
                // 通讯失败中终止测量
                AActivityFlags.IsExit = true;
                part.Pass = false;
                part.Status = PartStatus.PS_Error;
            }

            return Task.FromResult<AActivityFlags>(flag);
        }

        private PCDResponse Measure(Part part)
        {
            PCDResponse response;
            PCDRequest request = new PCDRequest();
            request.Part = part;
            response = _pcdmisService.MeasurePart(request);
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

        Task<AActivityFlags> ICMMControl.GotoSafePositionAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
