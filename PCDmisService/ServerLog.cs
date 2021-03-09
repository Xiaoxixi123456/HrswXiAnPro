using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PCDmisService
{
    public class ServerLog : BindableBase
    {
        public ObservableCollection<string> Messages { get; set; }

        private static ServerLog _inst = new ServerLog();
        public static ServerLog Logs => _inst ?? (_inst = new ServerLog());
        private ServerLog()
        {
            Messages = new ObservableCollection<string>();
        }

        public void AddLog(string message)
        {
            Messages.Add(message);
        }
    }
}
