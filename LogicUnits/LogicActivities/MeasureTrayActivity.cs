using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using System.Threading;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class MeasureTrayActivity : IAActivity<Tray>
    {
        private ISelectorAActivity<Part> _selector;
        private IAActivity<Part> _mesPartActivity;
        
        public MeasureTrayActivity(ICMMControl cmmControl)
        {
            _selector = new SelectorActivity<Part>();
            _mesPartActivity = new MeasurePartActivity(cmmControl);
        }

        public async Task<bool> ExecuteAsync(Tray obj, CancellationTokenSource cts)
        {
            bool success = false;
            while (true)
            {
                Part part = await _selector.ExecuteAsync(obj.Parts, cts);
                if (part == null)
                    break;
                success = await _mesPartActivity.ExecuteAsync(part, cts);
                if (!success)
                    break;
            }
            return success;
        }
    }
}
