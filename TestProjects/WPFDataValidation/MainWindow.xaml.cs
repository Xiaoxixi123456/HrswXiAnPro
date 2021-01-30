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

namespace WPFDataValidation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public Person person { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            person = new Person() { Name = "abc", Age = 20 };
            DataContext = person;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
           if (person.Validation())
            {
                MessageBox.Show("数据输入不正确.");
            }
        }
    }
}
