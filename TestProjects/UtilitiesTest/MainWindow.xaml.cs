using PostSharpWpf;
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

namespace UtilitiesTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public Person Person { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Person = new Person() { Name = "abc", Age = 30 };
            DataContext = Person;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Person.Age = 50;
            Person.Name = "GGG";
        }
    }
}
