using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MainApp.Utilities
{
    public class IpConfigs : BindableBase
    {
        [Bindable]
        public string PlcIp { get; set; } = "192.168.100.1";
        [Bindable]
        public string PcdmisIp { get; set; } = "localhost";
        [Bindable]
        public string CalypsoIp { get; set; } = "localhost";
        

        private static IpConfigs _inst = new IpConfigs();
        public static IpConfigs Inst => _inst ?? (_inst = new IpConfigs());
        private IpConfigs()
        { }

        public void LoadIpConfigs()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "IpConfigs.xml");
            if (!File.Exists(fileName))
            {
                return;
            }
            using (var sm = File.OpenRead(fileName))
            {
                XmlSerializer xs = new XmlSerializer(typeof(IpConfigs));
                _inst = xs.Deserialize(sm) as IpConfigs;
            }
        }

        public void SaveIpConfigs()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "IpConfigs.xml");
            using (var sm = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xs = new XmlSerializer(typeof(IpConfigs));
                xs.Serialize(sm, xs);
            }
        }

        public void SetupIpConfigs()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            //ClientSection client = config.GetSectionGroup("system.serviceModel").Sections["client"] as ClientSection;
        }
    }
}
