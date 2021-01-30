using PostSharp.Aspects;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PostSharpWpf
{
    [Serializable]
    public class PrismPropertyBindingAspect : LocationInterceptionAspect
    {
        private string[] _derivedProperties;
        public PrismPropertyBindingAspect(params string[] derived)
        {
            _derivedProperties = derived;
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            //base.OnSetValue(args);
            var oldValue = args.GetCurrentValue();
            var newValue = args.Value;
            if (oldValue != newValue)
            {
                args.ProceedSetValue();
                Type a = newValue.GetType().MakeByRefType();
                Type[] typs = new Type[] { a, a, typeof(string) };
                RaisePropertyChanged(args.Instance, args.LocationName);
            }
        }

        private void RaisePropertyChanged(object instance, string locationName)
        {
            var type = instance.GetType().BaseType;
            if (type != null)
            {
                //var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Where(f => f.Name == "SetProperty").ToList();
                //var mes = type.GetMethod("SetProperty", BindingFlags.Instance | BindingFlags.NonPublic, Type.DefaultBinder, new Type[] { typeof(int), typeof(int),typeof(string) }, null);
                
                var method = type.GetMethod("RaisePropertyChanged", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null)
                {
                    object[] pars = new object[]{ locationName };
                    method.Invoke(instance, pars);
                }
            }
        }
    }
}
