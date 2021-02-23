using Hrsw.XiAnPro.Models;
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
            loadTrayButton.IsEnabled = false;
            var eType = e.NewValue.GetType();
            if (eType == typeof(Tray))
            {
                var tray = (Tray)e.NewValue;
                if (tray.Status != TrayStatus.TS_Empty)
                {
                    TrayUIWithLight trayUI = new TrayUIWithLight();
                    trayUI.DataContext = tray;
                    ShowContent.Content = trayUI;
                }
            }
            else if (eType == typeof(Rack))
            {
                loadTrayButton.IsEnabled = true;
                Rack rack = (Rack)e.NewValue;
                RackUserControl rc = new RackUserControl();
                rc.DataContext = rack;
                ShowContent.Content = rc;
                var dataContext = DataContext as MainViewModel;
                dataContext.SelectedRack = rack;
            }
        }
    }
}
