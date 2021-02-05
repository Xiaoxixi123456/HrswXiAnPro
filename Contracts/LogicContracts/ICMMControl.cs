using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.LogicContracts
{
    public interface ICMMControl 
    {
        Task<AActivityFlags> GotoSafePositionAsync();
        Task<AActivityFlags> MeasurePartAsync(Part part);
        void PauseMeasure();
        void Stop();
    }
}
