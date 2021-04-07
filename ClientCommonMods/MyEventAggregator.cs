using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCommonMods
{
    public class MyEventAggregator : IEventAggregator
    {
        Dictionary<Type, object> events = new Dictionary<Type, object>();
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            return (TEventType)events[typeof(TEventType)];
        }

        private static MyEventAggregator _inst = new MyEventAggregator();
        public static MyEventAggregator Inst => _inst ?? (_inst = new MyEventAggregator());
        private MyEventAggregator()
        {
            events.Add(typeof(MainAndLogicUnitEvent), new MainAndLogicUnitEvent());
            events.Add(typeof(CmmErrorEvent), new CmmErrorEvent());
            events.Add(typeof(PlcErrorEvent), new PlcErrorEvent());
            events.Add(typeof(LoadEvent), new LoadEvent());
            events.Add(typeof(UnloadEvent), new UnloadEvent());
        }
    }
}
