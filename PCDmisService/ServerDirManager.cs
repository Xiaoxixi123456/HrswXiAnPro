using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PCDmisService
{
    public class ServerDirManager : BindableBase
    {
        private static ServerDirManager _inst;
        public static ServerDirManager Inst => _inst ?? (_inst = new ServerDirManager());
        private ServerDirManager() { }
        
        [Bindable]
        public string MeasureProgDirectory { get; set; }
        [Bindable]
        public string ResultProgDirectory { get; set; }
    }
}
