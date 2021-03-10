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
        Task<bool> GotoSafePositionAsync();
        Task<bool> MeasurePartAsync(Part part);
        bool ReleaseMeasure();
        void PauseMeasure();
        void Stop();
    }
}
