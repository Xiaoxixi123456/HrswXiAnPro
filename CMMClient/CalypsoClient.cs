using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;

namespace Hrsw.XiAnPro.CMMClient
{
    public class CalypsoClient : ICMMControl
    {
        private static CalypsoClient _inst = new CalypsoClient();
        public static CalypsoClient Inst => _inst ?? (_inst = new CalypsoClient());
        private CalypsoClient() 
        {

        }

        public bool Initial()
        {
            return true;
        }
        public Task<bool> AnalyseReportAsync(out bool pass)
        {
            pass = false;
            return Task.FromResult(true);
        }

        public Task<bool> GotoSafePositionAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> MeasurePartAsync(Part part)
        {
            Random rand = new Random();
            part.Id = rand.Next(1, 200);
            return Task.FromResult(true);
        }

        public void PauseMeasure()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        Task<AActivityFlags> ICMMControl.GotoSafePositionAsync()
        {
            throw new NotImplementedException();
        }

        Task<AActivityFlags> ICMMControl.MeasurePartAsync(Part part)
        {
            throw new NotImplementedException();
        }
    }
}
