using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.LogicContracts
{
    public interface ICMM
    {
        Task<bool> MeasureAsync(Part part, CancellationTokenSource cts);
        Task<bool> MeasureAsync(Part part);
        bool Measure(Part part);
        void Pause();
        void Stop();
        void Offline();
        void Online();
        void ClearError();
    }
}
