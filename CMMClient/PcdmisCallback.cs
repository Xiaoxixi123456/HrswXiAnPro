using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.CMMClients.PcdmisServiceReference;
using System.Diagnostics;
using Prism.Events;
using ClientCommonMods;

namespace Hrsw.XiAnPro.CMMClient
{
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single)]
    public class PcdmisCallback : IPCDmisServiceCallback
    {
        public bool MeasErrorFlag { get; set; }
        private PcdmisClient _pcdClient;

        // TODO 需要判断安全定位和测量工件出错后的处理方式
        public void SendMessage(PCDMessage response)
        {
            Debug.WriteLine(response.Message);
            MeasErrorFlag = response.Error;
            _pcdClient.CmmError = response.Error;
            _pcdClient.StatusMessage = response.Message;
            MyEventAggregator.Inst.GetEvent<CmmErrorEvent>().Publish(new CmmErrorStatus() { CmmNo = 0, Error = _pcdClient.CmmError});
        }

        public PcdmisCallback(PcdmisClient pcdClient)
        {
            _pcdClient = pcdClient;
        }
    }
}
