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
        protected bool _error;

        public event EventHandler ErrorEvent;

        public PlcControlBase()
        {
            _plcAccessor = PLCAccessor.Instance;
        }

        public async Task<bool> ExecuteAsync(Tray tray)
        {
            try
            {
                _error = false;
                Initialize(tray);
                await Startup(tray).ConfigureAwait(false);
                StartMonitor();
                await WaitComplete().ConfigureAwait(false);
                EndMonitor();
            }
            catch (InvalidOperationException pe)
            {
                EndMonitor();
                ClientLogs.Inst.AddLog(new ClientLog(pe.Message));
                PLCAccessor.Instance.Connected = false;
                _error = true;
            }
            return _error ? false : true;
        }

        public virtual Task WaitComplete()
        {
            return Task.Run(() =>
            {
                _error = false;
                while (true)
                {
                    // 出现错误退出等待
                    bool completed = OnCompleted();
                    if (completed || _error) break;

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
                    catch (Exception)
                    {
                        //throw;
                        ClientLogs.Inst.AddLog(new ClientLog(this.GetType().FullName + "OnError"));
                        PLCAccessor.Instance.Connected = false;
                        ErrorEvent?.Invoke(this, null );
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

        public virtual Task Startup(Tray tray)
        {
            throw new NotImplementedException();
        }
    }
}
