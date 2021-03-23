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
    public class SafeLocateActivity : IAActivity<Tray, bool>
    {
        public void Complete()
        {
            throw new NotImplementedException();
        }

        private ICMMControl _cmm;

        public SafeLocateActivity(ICMMControl cmmControl/*, ActivityController ac*/)
        {
            _cmm = cmmControl;
        }

        public async Task<bool> ExecuteAsync(Tray obj, CancellationTokenSource cts)
        {
            bool success = await _cmm.GotoSafePositionAsync();
            return success;
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
