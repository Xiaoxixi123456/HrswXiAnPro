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
        public ObservableCollection<string> Messages { get; set; }
        [Bindable]
        public string StatusMessage { get; set; }

        private static ClientLogs _inst = new ClientLogs();
        public static ClientLogs Inst => _inst ?? (_inst = new ClientLogs());
        private ClientLogs()
        {
            Messages = new ObservableCollection<string>();
            BindingOperations.EnableCollectionSynchronization(Messages, syncObj);
        }

        public void AddLog(string message)
        {
            Messages.Add(message);
        }
    }
}
