using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using System.Threading.Tasks;
using System.Threading;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class MeasurePartActivity : IAActivity<Part>
    {
        private ICMMControl _cmm;

        public MeasurePartActivity(ICMMControl cmm)
        {
            _cmm = cmm;
        }

        public MeasurePartActivity()
        {
            //_cmm = new CMMController();
        }

        public async Task<bool> ExecuteAsync(Part obj, CancellationTokenSource cts)
        {
            bool pass = false;

            bool success = await _cmm.MeasurePartAsync(obj);
            if (!success)
            {
                obj.Status = AAStatus.NoMeasured;
                return success;
            }
            obj.Status = AAStatus.Measured;
            success = await _cmm.AnalyseReportAsync(out pass);
            if (!success)
                return success;
            obj.Pass = pass;

            return success;
        }
    }
}
