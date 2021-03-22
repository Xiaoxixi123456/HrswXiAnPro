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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MainApp.Views
{
    /// <summary>
    /// StartupWainingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartupWainingWindow : Window
    {
        private DispatcherTimer _timer;
        private int _waitSecs;
        public StartupWainingWindow()
        {
            InitializeComponent();
            ContinueBtn.IsEnabled = false;
            _waitSecs = 3;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer(DispatcherPriority.Normal);
            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_waitSecs == 0)
            {
                _timer.Stop();
                ContinueBtn.Content = "确定";
                ContinueBtn.IsEnabled = true;
                ContinueBtn.Focus();
                return;
            }
            string btnContent = $"确认 {_waitSecs--}s";
            ContinueBtn.Content = btnContent;
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
