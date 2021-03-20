using Hrsw.XiAnPro.Models;
using MainApp.Utilities;
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
    /// RackUI.xaml 的交互逻辑
    /// </summary>
    public partial class RackUI : UserControl
    {
        public RackUI()
        {
            InitializeComponent();
        }

        private void RackTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //loadTrayButton.IsEnabled = true;
            if (e.NewValue == null)
                return;
            var eType = e.NewValue.GetType();
            if (eType == typeof(Tray))
            {
                var tray = (Tray)e.NewValue;
                var dataContext = DataContext as MainViewModel;
                dataContext.SelectedTrayInRack = tray;
                dataContext.SelectedTypeId = 0;
                if (tray.Status != TrayStatus.TS_Empty)
                {
                    TrayUIWithLight trayUI = new TrayUIWithLight();
                    trayUI.DataContext = tray;
                    ShowContent.Content = trayUI;
                }
            }
            else if (eType == typeof(Rack))
            {
                //loadTrayButton.IsEnabled = true;
                Rack rack = (Rack)e.NewValue;
                var dataContext = DataContext as MainViewModel;
                dataContext.SelectedRack = rack;
                dataContext.SelectedTypeId = 1;
                RackUserControl rc = new RackUserControl();
                RackViewModel rcVm = new RackViewModel();
                rcVm.Rack = rack;
                rcVm.Trays = dataContext.Trays;
                rc.DataContext = rcVm;
                ShowContent.Content = rc;
            }
        }

        private void loadTrayButton_MouseMove(object sender, MouseEventArgs e)
        {
            //OpInfomations.info = "往料库槽中装夹料盘";
            ((MainViewModel)DataContext).OpInfo = "往料库槽中装夹料盘";
        }

        private void loadTrayButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "";
        }

        private void unloadTrayButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "";
        }

        private void unloadTrayButton_MouseMove(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "往料库槽中卸载料盘";
        }

        private void startAutoflowButton_MouseMove(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "启动检测流程";
        }

        private void startAutoflowButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "";
        }

        private void stopButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "";
        }

        private void stopButton_MouseMove(object sender, MouseEventArgs e)
        {
            ((MainViewModel)DataContext).OpInfo = "停止检测流程";
        }
    }
}
