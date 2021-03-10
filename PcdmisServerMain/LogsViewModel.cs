using Hrsw.XiAnPro.PCDmisService;
using Hrsw.XiAnPro.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcdmisServerMain
{
    public class LogsViewModel : BindableBase
    {
        [Bindable]
        public ServerLog Logs { get; set; }

        public DelegateCommand ClearCommand { get; set; }

        public LogsViewModel()
        {
            ClearCommand = new DelegateCommand(ClearLogs);
            Logs = ServerLog.Logs;
        }

        private void ClearLogs()
        {
            Logs.Messages.Clear();
        }
    }
}
