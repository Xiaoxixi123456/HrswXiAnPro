using MahApps.Metro.Controls;
using MainApp.ViewModels;
using MainApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MainApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainViewModel MainViewModel { get; set; }
        private DispatcherTimer _runLedtimer;
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            MainViewModel.Dispatcher = Dispatcher;
            DataContext = MainViewModel;
            _runLedtimer = new DispatcherTimer(DispatcherPriority.DataBind);
            _runLedtimer.Tick += _runLedtimer_Tick;
        }

        private void _runLedtimer_Tick(object sender, EventArgs e)
        {
            if (MainViewModel.Started)
            {
                MainViewModel.RunLedIndex = (++MainViewModel.RunLedIndex) % 3;
            }
            else
            {
                MainViewModel.RunLedIndex = -1;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Window_Loaded 错误处理
            try
            {
                SplashWindow.Start();
                MainViewModel.Initial();
                _runLedtimer.Interval = TimeSpan.FromSeconds(1);
                _runLedtimer.Start();
                SplashWindow.Stop();
                Activate();
            }
            catch (Exception)
            {
                Application.Current.Shutdown(0);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Start();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            MainViewModel.Dispose();
        }

        private void RackUI_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
