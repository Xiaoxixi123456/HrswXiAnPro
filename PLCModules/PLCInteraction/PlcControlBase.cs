using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using PLCServices;
using System.Threading;

namespace Hrsw.XiAnPro.PLCInteraction
{
    public class PlcControlBase : IPlcControl
    {
        protected PLCAccessor _plcAccessor;

        public PlcControlBase()
        {
            _plcAccessor = PLCAccessor.Instance;
        }
        public async Task<bool> ExecuteAsync(Tray tray)
        {
            bool success;
            try
            {
                Initialize(tray);
                await Startup(tray).ConfigureAwait(false);
                success = await OnMonitor().ConfigureAwait(false);
            }
            catch (InvalidOperationException pe)
            {
                success = false;
            }
            return success;
        }

        public virtual bool OnCompleted()
        {
            throw new NotImplementedException();
        }

        public virtual bool OnError()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> OnMonitor()
        {
            return Task.Run(() =>
            {
                bool result;
                while (true)
                {
                    result = OnCompleted();
                    if (result)
                        break;
                    result = OnError();
                    if (!result)
                        break;
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                return result;
            });
        }

        public virtual void Initialize(Tray tray)
        {
            throw new NotImplementedException();
        }

        public virtual Task Startup(Tray tray)
        {
            throw new NotImplementedException();
        }
    }
}
