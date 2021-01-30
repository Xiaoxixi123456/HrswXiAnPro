using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace InvokeMethod
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int _tid;
        public int Tid
        {
            get { return _tid; }
            set
            {
                _tid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tid"));
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Tid = Dispatcher.Thread.ManagedThreadId;
            Dispatcher.InvokeAsync(MethodTest);
        }

        private void MethodTest()
        {
            while (true)
            {
                Thread.Sleep(2000);
            Tid = Thread.CurrentThread.ManagedThreadId;
            }
        }
    }
}
