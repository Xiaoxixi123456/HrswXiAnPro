using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.Utilities
{
    [Serializable]
    public class Bindable : LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            //base.OnSetValue(args);
            var oldValue = args.GetCurrentValue();
            var newValue = args.Value;
            if (oldValue != newValue)
            {
                args.ProceedSetValue();
                RaisePropertyChanged(args.Instance, args.LocationName);
            }
        }

        private void RaisePropertyChanged(object instance, string locationName)
        {
            var type = instance.GetType().BaseType;
            if (type != null)
            {
                var method = type.GetMethod("RaisePropertyChanged", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null)
                {
                    object[] pars = new object[] { locationName };
                    method.Invoke(instance, pars);
                }
            }
        }
    }
}
