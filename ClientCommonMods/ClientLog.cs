using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCommonMods
{
    public class ClientLog : BindableBase
    {
        [Bindable]
        public string Message { get; set; }
        [Bindable]
        public DateTime LogTime { get; set; }
        public ClientLog(string message)
        {
            LogTime = DateTime.Now;
            Message = message;
        }
    }
}
