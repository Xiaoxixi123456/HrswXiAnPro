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

namespace MainApp.Views
{
    /// <summary>
    /// TraysUI.xaml 的交互逻辑
    /// </summary>
    public partial class TraysUI : UserControl
    {
        public TraysUI()
        {
            InitializeComponent();
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "添加料盘并设置料盘参数";
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "";
        }

        private void Button_MouseMove_1(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "删除料盘";
        }

        private void Button_MouseLeave_1(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "";
        }

        private void Button_MouseMove_2(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "往料盘上装夹工件";
        }

        private void Button_MouseLeave_2(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "";
        }

        private void Button_MouseMove_3(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "从文件导入料盘";
        }

        private void Button_MouseLeave_3(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "";
        }
    }
}
