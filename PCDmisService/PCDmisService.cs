using Hrsw.XiAnPro.PCDmisImplement;
using Hrsw.XiAnPro.PCDmisServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using System.Threading;
using System.IO;
using Prism.Mvvm;
using Hrsw.XiAnPro.Utilities;

namespace Hrsw.XiAnPro.PCDmisService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
    public class PCDmisService : BindableBase, IPCDmisService, IDisposable
    {
        private IPCDmisCallback _pcdmisCallback;
        private PCDmisControl _pcdmisControl;
        private AutoResetEvent _are;
        private bool _completed;

        [Bindable]
        public string MeasProgDir { get; set; } = @"E:\WorkDirs\PcdmisFiles";
        [Bindable]
        public string MeasProgsFileName { get; set; } = "MeasProg.xml";
        [Bindable]
        public string ResultFilesDir { get; set; } = @"E:\WorkDirs\PcdmisFiles";
        [Bindable]
        public bool Connected { get; set; }
        [Bindable]
        public string StatusMessage { get; set; }

        public PCDmisService()
        {
            _are = new AutoResetEvent(false);
            _pcdmisControl = new PCDmisControl();
            _pcdmisControl.Executed += Executed;
            _pcdmisControl.ExecuteError += ExecuteError;
            _pcdmisControl.PcdmisMessageBox += MessageBoxEventHandler;
        }

        private void MessageBoxEventHandler(object sender, PcdmisEventArgs e)
        {
            // TODO 取消测量需人工干预是否进行下一个测量
            _completed = false;
            RespondMessage(e.Message);
            ServerLog.Logs.AddLog(e.Message);
        }

        private void ExecuteError(object sender, PcdmisEventArgs e)
        {
            // TODO 取消测量需人工干预是否进行下一个测量
            _completed = false;
            RespondMessage(e.Message);
            ServerLog.Logs.AddLog(e.Message);
        }

        private void Executed(object sender, PcdmisEventArgs e)
        {
            if (e.IsCancelled)
            {
                // TODO 取消测量需人工干预是否进行下一个测量
                _completed = false;
                RespondMessage(e.Message);
                ServerLog.Logs.AddLog(e.Message);
            }
            else
            {
                _completed = true;
                _are.Set();
            }
        }

        public void Initial()
        {
            try
            {
                _pcdmisControl.InitPcdmis();
                //MeasProgManager.Inst.SavFileName = Path.Combine(MeasProgDir, MeasProgsFileName);
                //MeasProgManager.Inst.LoadPrograms();
            }
            catch (PcdmisServiceException e)
            {
                RespondMessage(e.Message);
                ServerLog.Logs.AddLog(e.Message);
            }
        }

        public PCDResponse Connect()
        {
            _pcdmisCallback = OperationContext.Current.GetCallbackChannel<IPCDmisCallback>();
            
            PCDResponse response = new PCDResponse() { Success = true, Message = "" };
            if (_pcdmisCallback == null)
            {
                response.Success = false;
                response.Message = "回连失败";
            }
            Connected = response.Success;
            OperationContext.Current.Channel.Closed += Channel_Closed;
            OperationContext.Current.Channel.Faulted += Channel_Faulted;
            return response;
        }

        public PCDResponse Disconnect()
        {
            try
            {
                OperationContext.Current.Channel.Closed -= Channel_Closed;
                OperationContext.Current.Channel.Faulted -= Channel_Faulted;
            }
            catch
            {
            }
            _pcdmisCallback = null;
            PCDResponse response = new PCDResponse() { Success = true, Message = "" };
            Connected = false;
            return response;
        }

        private void Channel_Faulted(object sender, EventArgs e)
        {
            StatusMessage = "控制端错误";
            Connected = false;
            ServerLog.Logs.AddLog("控制端错误");
        }

        private void Channel_Closed(object sender, EventArgs e)
        {
            StatusMessage = "控制端关闭";
            Connected = false;
            ServerLog.Logs.AddLog("控制端关闭");
        }

        public PCDResponse GotoSafePostion()
        {
            throw new NotImplementedException();
        }

        public PCDResponse MeasurePart(PCDRequest request)
        {
            PCDResponse resp = null;
            try
            {
                string progName = SearchProgram(request.Part);

                GenerateMeasureParameterFile(request.Part);

                _are.Reset();
                bool success = _pcdmisControl.OpenProgram(progName);
                success = _pcdmisControl.ExecuteProgramAsync();
                _are.WaitOne();

                resp = EvalMeasure(_completed);
            }
            catch (PcdmisServiceException pe)
            {
                resp = new PCDResponse()
                {
                    Success = false,
                    Pass = false,
                    Message = pe.Message
                };
                ServerLog.Logs.AddLog(pe.Message);
            }
            return resp;
        }

        private void GenerateMeasureParameterFile(Part part)
        {
            // TODO 固定文件名
            string fileName = Path.Combine(MeasProgDir, "MeasParams.Par");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Base, {part.XOffset}, {part.YOffset}");
            File.WriteAllText(fileName, sb.ToString());
        }

        private PCDResponse EvalMeasure(bool completed)
        {
            PCDResponse resp = new PCDResponse();

            if (completed)
            {
                bool pass = EvalReport();
                resp.ReportFile = GetOutputFile();
                resp.Success = true;
                resp.Pass = pass;
                resp.Message = "测量完成。";
            }
            else
            {
                resp.Success = false;
                resp.Pass = false;
                resp.Message = "测量未完成。";
            }

            return resp;
        }

        private string GetOutputFile()
        {
            return _pcdmisControl.GetOutputFile();
        }

        private bool EvalReport()
        {
            var dims = _pcdmisControl.GetDimensions();
            if (dims == null)
                throw new PcdmisServiceException("测量完成，但无法评价尺寸。");
            foreach (var dim in dims)
            {
                if (dim.IsOutTol)
                    return false;
            }
            return true;
        }

        private string SearchProgram(Part part)
        {
            // 
            string prg = string.Empty;
            try
            {
                prg = MeasProgManager.Inst.GetMeasProg(part.Id);
            }
            catch (Exception ex)
            {
                throw new PcdmisServiceException(ex.Message);
            }
            return prg;
        }

        /// <summary>
        /// 服务器返回消息
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        private void RespondMessage(string message)
        {
            PCDMessage mage = new PCDMessage() { Result = true, Message = message };
            _pcdmisCallback?.SendMessage(mage);
        }

        public void Dispose()
        {
            if (_pcdmisControl != null)
            {
                _pcdmisControl.Close();
            }
            Disconnect();
        }
    }
}
