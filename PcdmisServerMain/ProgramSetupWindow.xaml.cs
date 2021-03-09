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
                    if (id == 0)
                    {
                        MessageBox.Show("ID不能为0", "警告", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                        return;
                    }
                    int count = vm.MeasProgs.Where(p => p.Id == id).Count();
                    if (count > 0)
                    {
                        MessageBox.Show("输入ID重复", "警告", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
            }
        }

    }
}
