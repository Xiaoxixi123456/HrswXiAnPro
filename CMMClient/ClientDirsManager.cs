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
        private static ClientDirsManager _inst = new ClientDirsManager();
        public static ClientDirsManager Inst => _inst ?? (_inst = new ClientDirsManager());

        private ClientDirsManager() { }

        [Bindable]
        public string PcdmisReportsDirectory { get; set; }
        = @"E:\WorkDirs\PcdmisFiles\DestReports";
        [Bindable]
        public string CalypsoReportsDirectory { get; set; } = @"E:\WorkDirs\PcdmisFiles\DestReports";
    }
}
