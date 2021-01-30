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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfListUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            DataContext = ViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;
            RackViewModel rvm = (RackViewModel)listBox.SelectedItem;
            rvm.Person = new Person() { Name = "AAAAAA", Age = 60 };
            rvm.UserControlUI = new UserControl1() { DataContext = rvm.Person };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;
            // 需要判断是否未空槽
            RackViewModel rvm = (RackViewModel)listBox.SelectedItem;
            rvm.Person.Name = "sdf";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                return;
            RackViewModel rvm = (RackViewModel)listBox.SelectedItem;
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                rvm.Person.Name = "New one";
                rvm.Person.Age = 20;/* = new Person() { Name = "new one", Age = 10 }*/;
            });
            //rvm.UserControlUI.DataContext = rvm.Person;
        }
    }
}
