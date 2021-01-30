using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicTest
{
    public class LogicCtrl
    {
        public List<IAAction> Actions { get; set; } = new List<IAAction>();
        CancellationTokenSource cts = new CancellationTokenSource();
        private int _currentActionId = 0;

        public void Initial()
        {

        }

        public async void Start()
        {
            while (true)
            {
                if (cts.IsCancellationRequested) break;
                bool result = await Actions[_currentActionId].Execute();

            }
        }

        public void Stop()
        {

        }
    }

}
