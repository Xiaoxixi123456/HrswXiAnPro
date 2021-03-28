using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PcdmisServerMain
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
            bool first;
            _appMutex = new Mutex(true, "PcdmisServerSingleAppMutex", out first);
            if (!first)
            {
                MessageBox.Show("服务器已经打开", "信息", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                _appMutex.Dispose();
                App.Current.Shutdown(0);
            }
        }
    }
}
