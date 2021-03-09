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
using System.Windows.Shapes;

namespace PcdmisServerMain
{
    /// <summary>
    /// ProgramSetupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProgramSetupWindow : Window
    {
        private int bkId;
        public ProgramSetupWindow()
        {
            InitializeComponent();
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ProgsSetupViewModel vm = DataContext as ProgsSetupViewModel;
            // 空行会自动添加ID = 0的MeasProg
            if (e.Column.DisplayIndex == 0)
            {
                var idStr = (e.EditingElement as TextBox).Text;
                int id;
                bool ok = int.TryParse(idStr, out id);
                if (ok)
                {
                    int count = vm.MeasProgs.Where(p => p.Id == id).Count();
                    if (count > 0)
                    {
                        MessageBox.Show("输入ID重复", "警告", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                        (e.EditingElement as TextBox).Text = bkId.ToString();
                    }
                }
            }
        }

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            ProgsSetupViewModel vm = DataContext as ProgsSetupViewModel;
            if (e.Column.DisplayIndex == 0)
            {
                bkId = vm.MeasProgs[e.Row.GetIndex()].Id;
            }
        }
    }
}
