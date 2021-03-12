using Hrsw.XiAnPro.CMMClients.PcdmisServiceReference;
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
    public class PcdmisClient : BindableBase, ICMMControl, IDisposable
    {
        private static PcdmisClient _inst = new PcdmisClient();
        public static PcdmisClient Inst => _inst ?? (_inst = new PcdmisClient());
        private PCDmisServiceClient _pcdmisService;
        private PcdmisCallback _pcdmisCallback;

        private PcdmisClient() { }
        private Timer _timer;

        [Bindable]
        public bool Connected { get; set; }

        public bool Initial()
        {
            bool result = true;
            //连接服务器端
            try
            {
                OpenPcdmisService();
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private void OpenPcdmisService()
        {
            _pcdmisCallback = new PcdmisCallback();
            _pcdmisService = new PCDmisServiceClient(new InstanceContext(_pcdmisCallback));
            _pcdmisService.InnerDuplexChannel.Faulted += InnerDuplexChannel_Faulted;
            _pcdmisService.InnerDuplexChannel.Opened += InnerDuplexChannel_Opened;
            _pcdmisService.InnerDuplexChannel.Closed += InnerDuplexChannel_Closed;
            _pcdmisService.Open();
            Connected = true;
        }

        private void InnerDuplexChannel_Closed(object sender, EventArgs e)
        {
            try
            {
                if (_pcdmisService.State == CommunicationState.Opened)
                {
                    _pcdmisService.Close();
                }
                else if (_pcdmisService.State == CommunicationState.Faulted)
                {
                    _pcdmisService.Abort();
                }
                _pcdmisService.InnerDuplexChannel.Faulted -= InnerDuplexChannel_Faulted;
                _pcdmisService.InnerDuplexChannel.Opened -= InnerDuplexChannel_Opened;
                _pcdmisService.InnerDuplexChannel.Closed -= InnerDuplexChannel_Closed;
                _pcdmisService = null;
            }
            catch (Exception)
            {
            }
            Connected = false;
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
            ClosePcdmisService();
        }

        public bool Offline()
        {
            ClosePcdmisService();
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
    }
}
