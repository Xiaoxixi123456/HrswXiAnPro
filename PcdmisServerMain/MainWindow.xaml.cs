using Hrsw.XiAnPro.ServerCommonMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PcdmisServerMain
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon notifyIcon;
        private WindowState ws;
        private WindowState wsl;

        public ViewModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ServerLog.Logs.Dispatcher = Dispatcher;
            ViewModel = new ViewModel();
            DataContext = ViewModel;
            desIcon();
            //保证窗体显示在上方。
            wsl = WindowState;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ViewModel.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
        }

        public void desIcon()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.BalloonTipText = "Pcdmis控制服务器"; //设置程序启动时显示的文本
            this.notifyIcon.Text = "Pcdmis服务器";//最小化到托盘时，鼠标点击时显示的文本
            this.notifyIcon.Icon = new System.Drawing.Icon("ControlServer.ico");//程序图标
            this.notifyIcon.Visible = true;
            //打开菜单项
            System.Windows.Forms.MenuItem open = new System.Windows.Forms.MenuItem("Open");
            open.Click += new EventHandler(ShowWnd);
            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("Exit");
            exit.Click += new EventHandler(CloseWnd);
            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { open, exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);
            notifyIcon.MouseDoubleClick += OnNotifyIconDoubleClick;
            this.notifyIcon.ShowBalloonTip(1000);
        }

        private void CloseWnd(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ShowWnd(object sender, EventArgs e)
        {
            //this.Visibility = System.Windows.Visibility.Visible;
            //this.ShowInTaskbar = true;
            //this.WindowState = WindowState.Normal;
            //this.Activate();
            this.Show();
            WindowState = wsl;
        }

        private void OnNotifyIconDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
            WindowState = wsl;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ws = WindowState;
            if (ws == WindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ViewModel.PcdmisService.CloseChannel();
        }
    }
}
