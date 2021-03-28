using Hrsw.XiAnPro.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfigApp
{
    public class ViewModel : BindableBase
    {
        [Bindable]
        public string PcdmisIp { get; set; }
        [Bindable]
        public string CalypsoIp { get; set; }
        [Bindable]
        public string PlcIp { get; set; }

        private string ConfigFile = "MainApp.exe.config";

        public DelegateCommand AppleCommand
        { get; set; }

        public ViewModel()
        {
            AppleCommand = new DelegateCommand(Apple);
            LoadConfigs();
        }

        private void Apple()
        {
            SaveConfigs();
        }

        private void LoadConfigs()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + ConfigFile);
            if (File.Exists(configPath))
            {
                XDocument doc = XDocument.Load(configPath);
                var endpoints = doc.Descendants("endpoint").ToList();
                var pcdAttr = endpoints[0].Attribute(XName.Get("address"));
                var pcdStrs = pcdAttr.Value.Split(":/".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                PcdmisIp = pcdStrs[1];
                var calAttr = endpoints[2].Attribute(XName.Get("address"));
                var calStrs = calAttr.Value.Split(":/".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                CalypsoIp = calStrs[1];


                var plcSetting = doc.Descendants("setting").Where(p => p.Attribute("name").Value == "PlcIp").Single();
                PlcIp = plcSetting.Value;
            }
        }

        private void SaveConfigs()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + ConfigFile);
            if (File.Exists(configPath))
            {
                XDocument doc = XDocument.Load(configPath);
                var endpoints = doc.Descendants("endpoint").ToList();
                // pcdmis
                string pcdBaseAddress = "net.tcp://" + PcdmisIp + ":";
                var pcdAttr = endpoints[0].Attribute(XName.Get("address"));
                var pcdStrs = pcdAttr.Value.Split(":/".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var hrexStrs = pcdStrs.Skip(2).ToList();
                string hrexStr = string.Join("/", hrexStrs);
                string newPcdAddress = pcdBaseAddress + hrexStr;
                pcdAttr.SetValue(newPcdAddress);

                var pcdFsAttr = endpoints[1].Attribute("address");
                var pcdFsStrs = pcdFsAttr.Value.Split(":/".ToArray(), StringSplitOptions.RemoveEmptyEntries).Skip(2).ToList();
                hrexStr = string.Join("/", pcdFsStrs);
                newPcdAddress = pcdBaseAddress + hrexStr;
                pcdFsAttr.SetValue(newPcdAddress);

                // calypso
                string baseAddress = "net.tcp://" + CalypsoIp + ":";
                var calAttr = endpoints[2].Attribute(XName.Get("address"));
                var calStrs = calAttr.Value.Split(":/".ToArray(), StringSplitOptions.RemoveEmptyEntries).Skip(2).ToList();
                hrexStr = string.Join("/", calStrs);
                string newAddress = baseAddress + hrexStr;
                calAttr.SetValue(newAddress);

                var calFsAttr = endpoints[3].Attribute(XName.Get("address"));
                var calFsStrs = calFsAttr.Value.Split(":/".ToArray(), StringSplitOptions.RemoveEmptyEntries).Skip(2).ToList();
                hrexStr = string.Join("/", calFsStrs);
                newAddress = baseAddress + hrexStr;
                calFsAttr.SetValue(newAddress);


                var plcSetting = doc.Descendants("setting").Where(p => p.Attribute("name").Value == "PlcIp").Single();
                plcSetting.SetValue(PlcIp);

                using (var sm = new FileStream(configPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    doc.Save(sm);
                }
            }
        }
    }
}
