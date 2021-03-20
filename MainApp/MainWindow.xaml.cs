using MahApps.Metro.Controls;
using MainApp.ViewModels;
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

namespace MainApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainViewModel MainViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO Window_Loaded 错误处理
            try
            {
                MainViewModel.Initial();
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
