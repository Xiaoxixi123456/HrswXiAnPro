using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MainApp.Views
{
    /// <summary>
    /// SplashWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        static SplashWindow sw;
        internal static void Start()
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                sw = new SplashWindow();
                sw.ShowDialog();
                //sw.Activate();
            }));

            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        internal static void Stop()
        {
            if (sw != null)
            {
                sw.Dispatcher.BeginInvoke((Action)(() => sw.Close()));
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
