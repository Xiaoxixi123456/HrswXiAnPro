using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.ServerCommonMod
{
    public class ServerDirManager : BindableBase
    {
        private static ServerDirManager _inst;
        public static ServerDirManager Inst => _inst ?? (_inst = new ServerDirManager());
        private ServerDirManager() { }
        
        [Bindable]
        public string MeasureProgDirectory { get; set; }
        [Bindable]
        public string ResultDirectory { get; set; }
        [Bindable]
        public string SavFileName { get; set; } = "Dirs.xml";

        public void SaveDirs()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SavFileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            File.WriteAllLines(SavFileName, new string[] { MeasureProgDirectory, ResultDirectory });
        }

        public void LoadDirs()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SavFileName);
            if (!File.Exists(fileName))
            {
                return;
            }

           var files = File.ReadAllLines(fileName);
            MeasureProgDirectory = files[0];
            ResultDirectory = files[1];
        }
    }
}
