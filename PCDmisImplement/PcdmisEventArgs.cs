using System;

namespace Hrsw.XiAnPro.PCDmisImplement
{
    public class PcdmisEventArgs : EventArgs
    {
        public bool IsCancelled { get; set; } = false;
        public string Message { get; private set; }
        public PcdmisEventArgs() { }

        public PcdmisEventArgs(string pcdmisMessage)
        {
            Message = pcdmisMessage;
        }

        public PcdmisEventArgs(bool isCancelled, string pcdmisMessage)
        {
            IsCancelled = isCancelled;
            Message = pcdmisMessage;
        }

    }
}