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
        //private int _currentId;
        //private Part _part;
        //private IAActivity<Part> _measurePartAActivity;
        ////private ISelector<Tray, Part> _partSelector;

        //public MeasureTrayActivity(IAActivity<Part> cmm/*, ISelector<Tray, Part> selector*/)
        //{
        //    _currentId = 0;
        //    _measurePartAActivity = cmm;
        //    _partSelector = selector;
        //}

        //public async Task<bool> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        //{
        //    _currentId = 0;
        //    bool success = true;
        //    while (true)
        //    {
        //        if (cts.IsCancellationRequested)
        //        {
        //            success = false;
        //            break;
        //        }
        //        Part part = await _partSelector.SelectAsync(tray, cts);
        //        if (part == null)
        //            break;
        //        _part = part;
        //        success = await _measurePartAActivity.ExecuteAsync(_part, cts);
        //        if (!success)
        //            break;
        //    }
        //    return success;
        //}

        //public void Stop()
        //{
        //    throw new NotImplementedException();
        //}
        public Task<bool> ExecuteAsync(Tray obj, CancellationTokenSource cts)
        {
            throw new NotImplementedException();
        }
    }
}
