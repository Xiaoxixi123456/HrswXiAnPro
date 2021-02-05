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

namespace Hrsw.XiAnPro.PCDmisService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
    public class PCDmisService : IPCDmisService
    {
        private IPCDmisCallback _pcdmisCallback;
        private PCDmisControl _pcdmisControl;
        private AutoResetEvent _are;
        private bool _completed;
        private bool _nextPart;

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
        }

        private void ExecuteError(object sender, PcdmisEventArgs e)
        {
            // TODO 取消测量需人工干预是否进行下一个测量
            _completed = false;
            RespondMessage(e.Message);
        }

        private void Executed(object sender, PcdmisEventArgs e)
        {
            if (e.IsCancelled)
            {
                // TODO 取消测量需人工干预是否进行下一个测量
                _completed = false;
                RespondMessage(e.Message);
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
            }
            catch (PcdmisServiceException e)
            {
                RespondMessage(e.Message);
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
            return response;
        }

        public PCDResponse Disconnect()
        {
            _pcdmisCallback = null;
            PCDResponse response = new PCDResponse() { Success = true, Message = "" };
            return response;
        }

        public PCDResponse GotoSafePostion()
        {
            throw new NotImplementedException();
        }

        public PCDResponse MeasurePart(PCDRequest request)
        {
            PCDResponse resp = new PCDResponse()
            {
                Success = true,
                Pass = true,
                Message = ""
            };
            // TODO 生成偏置文件
            // 在零件程序目录中查找零件程序
            // 调用零件程序
            // 异步执行零件程序
            // 等待程序执行结束
            // 判读测量结果
            // 返回结果
            string progName = SearchProgram(request.Part);
            try
            {
                _are.Reset();
                bool success = _pcdmisControl.OpenProgram(progName);
                success = _pcdmisControl.ExecuteProgramAsync();
                _are.WaitOne();
                resp = EvalMeasure(_completed);
            }
            catch (PcdmisServiceException pe)
            {
                resp.Success = false;
                resp.Pass = false;
                resp.Message = pe.Message;
            }
            return resp;
        }

        private PCDResponse EvalMeasure(bool completed)
        {
            PCDResponse resp = new PCDResponse()
            {
                Success = true,
                Pass = true,
                Message = ""
            };

            if (completed)
            {
                bool pass = EvalReport();
                resp.Success = true;
                resp.Pass = pass;
                resp.Message = "测量完成。";
            }
            else
            {
                resp.Success = false;
                resp.IsNext = _nextPart;
                resp.Pass = false;
                resp.Message = "测量未完成。";
            }

            return resp;
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
            return @"E:\WorkDirs\PcdmisFiles\SafePlane4.prg";
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

        public void Next()
        {
            _nextPart = true;
            _are.Set();
        }

        public void Retry()
        {
            _nextPart = false;
            _are.Set();
        }
    }
}
