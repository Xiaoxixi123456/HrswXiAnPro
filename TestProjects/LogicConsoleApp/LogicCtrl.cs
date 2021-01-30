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
            Actions.Add(new UpAction());
            Actions.Add(new MeasAction());
            Actions.Add(new DownAction());
        }

        public async void Start()
        {
            while (true)
            {
                if (cts.IsCancellationRequested) break;
                //bool result = await Actions[_currentActionId].ExecuteAsync();
                bool result = Actions[_currentActionId].Execute();
                if (result)
                {
                   _currentActionId =  (_currentActionId + 1) % 3;
                }
            }
        }

        public void Stop()
        {
            cts.Cancel();
        }
    }

}
