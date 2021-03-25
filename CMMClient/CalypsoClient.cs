using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.CMMClients.CalypsoServiceReference;
using Hrsw.XiAnPro.CMMClients;
using System.ServiceModel;
using Prism.Mvvm;
using Hrsw.XiAnPro.Utilities;
using System.Threading;
using ClientCommonMods;
using Hrsw.XiAnPro.PLCInteraction;
using System.IO;

namespace Hrsw.XiAnPro.CMMClient
{
    public class CalypsoClient : BindableBase, ICMMControl, IDisposable
    {
        private static CalypsoClient _inst = new CalypsoClient();

        public event EventHandler OfflineEvent;
        public event EventHandler OnlineEvent;

        private CalypsoServiceClient _calypsoServiceClient;
        private ReportFileTransfer _reportFileTransfer;
        private CalypsoPlcController _calypsoPlcController;
        private Timer _timer;

        [Bindable]
        public bool Connected { get; set; }

        public static CalypsoClient Inst => _inst ?? (_inst = new CalypsoClient());
        private CalypsoClient()
        {
            Connected = false;
            _calypsoPlcController = new CalypsoPlcController();
        }

        public bool Initial()
        {
            bool result = true;
            try
            {
                OpenCalypsoService();
                OpenTransferReportsService();
            }
            catch (Exception)
            {
                result = false;
                Connected = false;
            }
            return result;
        }

        private void OpenCalypsoService()
        {
            _calypsoServiceClient = new CalypsoServiceClient();
            _calypsoServiceClient.InnerChannel.Opened += InnerChannel_Opened;
            _calypsoServiceClient.InnerChannel.Closed += InnerChannel_Closed;
            _calypsoServiceClient.InnerChannel.Faulted += InnerChannel_Faulted;
            _calypsoServiceClient.Open();
            Connected = true;
        }

        private void InnerChannel_Faulted(object sender, EventArgs e)
        {
            _calypsoServiceClient.InnerChannel.Abort();
        }

        private void InnerChannel_Closed(object sender, EventArgs e)
        {
            _calypsoServiceClient.InnerChannel.Opened -= InnerChannel_Opened;
            _calypsoServiceClient.InnerChannel.Closed -= InnerChannel_Closed;
            _calypsoServiceClient.InnerChannel.Faulted -= InnerChannel_Faulted;
            _calypsoServiceClient = null;
        }

        private void InnerChannel_Opened(object sender, EventArgs e)
        {

        }

        private void OpenTransferReportsService()
        {
            string root = ClientDirsManager.Inst.CalypsoReportsDirectory;
            _reportFileTransfer = new ReportFileTransfer("Calypso");
            _reportFileTransfer.Initialize("CalypsoFileServiceEp");
            _reportFileTransfer.LaunchTransferProcess();
        }

        public void CloseServics()
        {
            CloseCalypsoService();
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

        public void CloseCalypsoService()
        {
            try
            {
                if (_calypsoServiceClient != null)
                {
                    if (_calypsoServiceClient.State == CommunicationState.Faulted)
                    {
                        _calypsoServiceClient.Abort();
                    }
                    else if (_calypsoServiceClient.State == CommunicationState.Opened)
                    {
                        _calypsoServiceClient.Close();
                    }
                }
            }
            catch (Exception)
            {
                // 关闭通讯失败
                ClientLogs.Inst.AddLog(new ClientLog("关闭calypso通讯失败"));
                _calypsoServiceClient.Abort();
            }
            Connected = false;
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
            bool success = true;
            try
            {
                part.Status = PartStatus.PS_Measuring;
                part.Flag = 0;
                _timer = new Timer(TestBack/*_ => part.Flag = ++part.Flag % 2*/, part, 0, 2000);

                //await Task.Delay(5000);
                success = await MeasurePart(part);

                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                part.Flag = 0;
                part.Status = success ? PartStatus.PS_Measured : PartStatus.PS_Error;
            }
            catch (Exception ex)
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

        private async Task<bool> SetMeasureParams(Part part)
        {
            CalypsoRequest calReq = new CalypsoRequest();
            calReq.Part = part;
            bool success = await _calypsoServiceClient.MakeOffsetFileAsync(calReq);
            return success;

        }

        private async Task GetReportFileName(Part part)
        {
            try
            {
                string fileName = await _calypsoServiceClient.GetReportFilenameAsync();
                part.ResultFile = Path.GetFileName(fileName);
            }
            catch (Exception)
            {
                ClientLogs.Inst.AddLog(new ClientLog("calypso报告文件名未找到"));
                part.ResultFile = "";
            }
        }

        private async Task<bool> MeasurePart(Part part)
        {
            bool success = await SetMeasureParams(part);
            if (!success) return success;

            success = await _calypsoPlcController.MeasurePartAsync(part);
            if (!success) return success;

            await GetReportFileName(part);
            //part.ResultFile = Path.GetFileName(fileName);

            return success;
        }


        private void TestBack(object obj)
        {
            Part part = (Part)obj;
            part.Flag = ++part.Flag % 2;
        }

        public void PauseMeasure()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public bool ReleaseMeasure()
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
