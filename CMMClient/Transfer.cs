using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.CMMClients
{
    public class Transfer<T>
    {
        protected BlockingCollection<T> _blockQuene;
        public event EventHandler<AutoControlEventArgs> TransferEvent;

        protected void OnDimTrsfEvent(string type, string message)
        {
            AutoControlEventArgs acArgs = new AutoControlEventArgs() { EventType = type, Message = message, EvtTime = DateTime.Now };
            TransferEvent?.Invoke(this, acArgs);
        }

        public Transfer() { }

        public virtual void LaunchTransferProcess() { }

        public virtual void StopStopTransferProcess()
        {
            _blockQuene.CompleteAdding();
        }

        public virtual void Next(T item)
        {
            if (!_blockQuene.IsAddingCompleted)
                _blockQuene.Add(item);
        }
    }
}
