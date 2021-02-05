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
    public class MeasureTrayActivity : IAActivity<Tray, AActivityFlags>
    {
        private IAActivity<Tray, Part> _selector;
        private IAActivity<Part, AActivityFlags> _mesPartActivity;
        
        public MeasureTrayActivity(ICMMControl cmmControl)
        {
            _selector = new PartSelectActivity();
            _mesPartActivity = new MeasurePartActivity(cmmControl);
        }

        public async Task<AActivityFlags> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            AActivityFlags resp = new AActivityFlags() { Next = true };
            AActivityFlags.IsExit = false;
            Part part = null;
            while (true)
            {
                if (cts.IsCancellationRequested || AActivityFlags.IsExit)
                    break;
                if (resp.Next)
                {
                    part = await _selector.ExecuteAsync(tray, cts);
                }
                if (part == null 
                    || cts.IsCancellationRequested 
                    || AActivityFlags.IsExit)
                    break;
                resp = await _mesPartActivity.ExecuteAsync(part, cts);
                if (AActivityFlags.IsExit)
                    break;
            }
            return resp;
        }
    }
}
