using MainApp.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MainApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private Mutex _appMutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            bool noexist;
            _appMutex = new Mutex(true, "SingleApp", out noexist);
            if (!noexist)
            {
                MessageBox.Show("应用已经打开", "信息", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                _appMutex.Dispose();
                App.Current.Shutdown(0);
                return;
            }
            //IpConfigs.Inst.SetupIpConfigs();
        }
    }
}
