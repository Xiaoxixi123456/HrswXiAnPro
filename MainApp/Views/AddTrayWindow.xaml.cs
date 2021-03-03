using Hrsw.XiAnPro.Models;
using MainApp.ViewModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MainApp.Views
{
    /// <summary>
    /// AddTrayWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddTrayWindow : Window
    {
        public ObservableCollection<Tray> Trays { get; set; }
        public TrayViewModel TrayViewModel { get; set; }

        public AddTrayWindow()
        {
            InitializeComponent();
            TrayViewModel = new TrayViewModel();
            DataContext = TrayViewModel;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Trays.Where(t => t.Category == TrayViewModel.Category && t.TrayNb == TrayViewModel.TrayNb).Count() != 0)
            {
                return;
            }
            Tray tray = new Tray()
            {
                Id = Trays.Count + 1,
                // 表示未放置到料架上
                Status = TrayStatus.TS_Idle,
                UseCmmNo = -1,
                Category = TrayViewModel.Category,
                TrayNb = TrayViewModel.TrayNb,
                CmmNo = TrayViewModel.CmmNo,
                SlotNb = -1,
                ColumnCount = TrayViewModel.ColumnCount,
                RowCount = TrayViewModel.RowCount,
                ColumnOffset = TrayViewModel.ColumnOffset,
                RowOffset = TrayViewModel.RowOffset,
                BaseColumnOffset = TrayViewModel.BaseColumnOffset,
                BaseRowOffset = TrayViewModel.BaseRowOffset,
                Placed = false,
                Parts = new ObservableCollection<Part>()
            };
            for (int i = 0; i < tray.ColumnCount * tray.RowCount; i++)
            {
                tray.Parts.Add(new Part() { Id = i, SlotNb = i + 1, Status = PartStatus.PS_Empty });
            }
            Trays.Add(tray);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
