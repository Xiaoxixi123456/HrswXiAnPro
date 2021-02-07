using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.LogicContracts
{
    public interface IPlcControl
    {
        Task<bool> ExecuteAsync(Tray tray);
        //void Setup(Tray tray);
        //Task Startup();
        //Task<bool> OnMonitor();
        //bool OnCompleted();
        //bool OnError();
    }
}
