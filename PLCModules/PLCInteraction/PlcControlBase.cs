using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using PLCServices;
using System.Threading;
using ClientCommonMods;

namespace Hrsw.XiAnPro.PLCInteraction
{
    public class PlcControlBase : IPlcControl
    {
        protected PLCAccessor _plcAccessor;
        protected CancellationTokenSource _cts;
        protected int _dbNumber;
        private bool _error;

        public event EventHandler ErrorEvent;

        public PlcControlBase()
        {
            _plcAccessor = PLCAccessor.Instance;
        }

        public async Task<bool> ExecuteAsync(Tray tray)
        {
            try
            {
                Initialize(tray);
                StartMonitor();
                Startup(tray);
                await WaitComplete().ConfigureAwait(false);
                EndMonitor();
            }
            catch (InvalidOperationException pe)
            {
                ClientLogs.Inst.AddLog(new ClientLog(pe.Message));
                _error = true;
            }
            return _error ? false : true;
        }

        public virtual Task WaitComplete()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    // TODO 出现错误退出等待
                    bool completed = OnCompleted();
                    if (completed) break;

                    Task.Delay(1000).Wait();
                }
            });
        }

        public virtual void OnError()
        {
            throw new NotImplementedException();
        }

        public virtual bool OnCompleted()
        {
            throw new NotImplementedException();
        }

        // 检测到上下料错误抛出异常
        // TODO 外部函数无法接到异常
        public virtual void StartMonitor()
        {
            _cts = new CancellationTokenSource();
            // 
            Task.Run(() =>
            {
                while (true)
                {
                    if (_cts.IsCancellationRequested) break;

                    try
                    {
                        OnError();
                    }
                    catch (Exception ex)
                    {
                        // TODO 错误出现时抛出异常
                        //throw;
                        ClientLogs.Inst.AddLog(new ClientLog(this.GetType().FullName + "OnError"));
                    }

                    Task.Delay(1000).Wait();
                }
            }, _cts.Token);
        }

        public virtual void EndMonitor()
        {
            _cts?.Cancel();
        }

        public virtual void Initialize(Tray tray)
        {
            _dbNumber = tray.UseCmmNo == 0 ? 1 : 2;
        }

        public virtual void Startup(Tray tray)
        {
            throw new NotImplementedException();
        }
    }
}
