using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace ClientCommonMods
{
    public class ClientLogs : BindableBase
    {
        public Dispatcher Dispatcher { get; set; }
        private object syncObj = new object();
        [Bindable]
        public ObservableCollection<ClientLog> Logs { get; set; }
        [Bindable]
        public string StatusMessage { get; set; }
        [Bindable]
        public TimeSpan RetainTimeSpan { get; set; }


        private static ClientLogs _inst = new ClientLogs();
        public static ClientLogs Inst => _inst ?? (_inst = new ClientLogs());
        private ClientLogs()
        {
            Logs = new ObservableCollection<ClientLog>();
        }

        public void Init()
        {
            Dispatcher.BeginInvoke((Action)(() => BindingOperations.EnableCollectionSynchronization(Logs, syncObj)));
        }

        public void AddLog(ClientLog log)
        {
            Logs.Add(log);
        }

        public void LoadLogs()
        {

        }

        public void SaveLogs()
        {

        }
    }
}
