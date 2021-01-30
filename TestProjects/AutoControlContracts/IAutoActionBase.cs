using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.AutoControlContracts
{
    public interface IAutoActionBase
    {
        bool Execute();
        bool Stop();
        void Pause();
    }
}
