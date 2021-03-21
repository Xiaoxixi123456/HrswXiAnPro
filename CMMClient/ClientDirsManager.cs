using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Bindable]
        public string SavFileName { get; set; } = "ClientDirs.xml";

        public string GetReportDirectory(string cmmName)
        {
            if (string.Compare(cmmName, "Pcdmis", true) == 0)
            {
                return PcdmisReportsDirectory;
            }
            else if (string.Compare(cmmName, "Calypso", true) == 0)
            {
                return CalypsoReportsDirectory;
            }
            else
            {
                return "";
            }
        }

        public void SaveDirs()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SavFileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            File.WriteAllLines(SavFileName, new string[] { PcdmisReportsDirectory, CalypsoReportsDirectory });
        }

        public void LoadDirs()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SavFileName);
            if (!File.Exists(fileName))
            {
                return;
            }

            var files = File.ReadAllLines(fileName);
            PcdmisReportsDirectory = files[0];
            CalypsoReportsDirectory = files[1];
        }
    }
}
