﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.CMMClients.PcdmisServiceReference;

namespace Hrsw.XiAnPro.CMMClient
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single)]
    public class PcdmisCallback : IPCDmisServiceCallback
    {
        public void SendMessage(PCDMessage response)
        {
            throw new NotImplementedException();
        }
    }
}
