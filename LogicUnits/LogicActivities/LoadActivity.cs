using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Hrsw.XiAnPro.Models;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class LoadActivity : IAActivity<Tray, bool>
    {
        public void Complete()
        {
            throw new NotImplementedException();
        }

        //IPLCLoad _plcLoad;

        public Task<bool> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            //bool success = await Task.Run(() =>
            //{
            //    tray.Id = 666;
            //    return true;
            //});
            //return success;
            throw new NotImplementedException();

        }

        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Retry()
        {
            throw new NotImplementedException();
        }
    }
}
