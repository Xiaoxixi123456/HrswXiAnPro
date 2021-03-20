using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MainApp.Utilities
{

    public class MainConfigManager : BindableBase
    {
        [Bindable]
        public UseCmmConfigs cmmConfigs { get; set; }

        public MainConfigManager()
        {
            cmmConfigs = new UseCmmConfigs()
            {
                UseCalypso = false,
                UsePcdmis = false
            };
        }

        public void LoadConfigs()
        {
            LoadUseCmmsConfig();
        }

        public void SaveConfigs()
        {
            SaveUseCmmsConfig();
        }
        private void LoadUseCmmsConfig()
        {
            UseCmmConfigs cc = null;
            try
            {
                using (var sm = File.OpenRead("cmmConfigs.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(UseCmmConfigs));
                    cc = xs.Deserialize(sm) as UseCmmConfigs;
                }
            }
            catch (Exception)
            {
            }
            if (cc != null)
            {
                cmmConfigs = cc;
            }
        }

        private void SaveUseCmmsConfig()
        {
            using (var sm = new FileStream("cmmConfigs.xml", FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xs = new XmlSerializer(typeof(UseCmmConfigs));
                xs.Serialize(sm, cmmConfigs);
            }
        }
    }
}
