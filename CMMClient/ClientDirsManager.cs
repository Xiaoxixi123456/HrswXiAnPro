using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.CMMClients
{
    public class ClientDirsManager : BindableBase
    {
        [Bindable]
        public static string PcdmisReportsDirectory { get; set; }
        = @"E:\PcdmisFiles\DestReports";
        [Bindable]
        public static string CalypsoReportsDirectory { get; set; }
    }
}
