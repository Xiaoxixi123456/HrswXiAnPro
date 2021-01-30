using Hrsw.XiAnPro.LogicControls;
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

namespace AutoFlowBindingTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel ViewModel { get; set; }
        public TestBindable TVM { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            TVM = new TestBindable();
            DataContext = ViewModel;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Start();
            TVM.Tray = new Hrsw.XiAnPro.Models.Tray() { Id = 666 } ;
        }
    }
}
