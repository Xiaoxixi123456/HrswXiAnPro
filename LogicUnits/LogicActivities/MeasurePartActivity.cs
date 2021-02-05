using Hrsw.XiAnPro.LogicContracts;
using Hrsw.XiAnPro.Models;
using System.Threading.Tasks;
using System.Threading;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class MeasurePartActivity : IAActivity<Part, AActivityFlags>
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

        public async Task<AActivityFlags> ExecuteAsync(Part obj, CancellationTokenSource cts)
        {
            AActivityFlags astatus = await _cmm.MeasurePartAsync(obj);
            return astatus;
        }
    }
}
