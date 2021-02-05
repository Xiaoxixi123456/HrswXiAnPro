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
    public class LoadActivity : IAActivity<Tray, AActivityFlags>
    {
        //IPLCLoad _plcLoad;
 
        public async Task<AActivityFlags> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            //bool success = await Task.Run(() =>
            //{
            //    tray.Id = 666;
            //    return true;
            //});
            //return success;
            throw new NotImplementedException();

        }
    }
}
