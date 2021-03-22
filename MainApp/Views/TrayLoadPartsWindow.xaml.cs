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
using System.Windows.Shapes;

namespace MainApp.Views
{
    /// <summary>
    /// TrayLoadPartsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TrayLoadPartsWindow : Window
    {
        public TrayLoadPartsWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TrayPartListbox.SelectAll();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TrayPartListbox.UnselectAll();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            partsListBox.UnselectAll();
        }

        private void TrayPartListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PartsOfTrayViewModel ptvm = (DataContext as PartsOfTrayViewModel);
            if (TrayPartListbox.SelectedItems == null
                || TrayPartListbox.SelectedItems.Count == 0)
            {
                ptvm.SelectedNumOfPartsInTray = 0;
                ptvm.SelectedPartsOfTray.Clear();
                return;
            }
            ptvm.SelectedPartsOfTray.Clear();
            foreach (var item in TrayPartListbox.SelectedItems)
            {
                ptvm.SelectedPartsOfTray.Add((Part)item);
            }
            ptvm.SelectedNumOfPartsInTray = ptvm.SelectedPartsOfTray.Count;
        }

        private void partsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PartsOfTrayViewModel ptvm = (DataContext as PartsOfTrayViewModel);
            if (partsListBox.SelectedItems == null
                || partsListBox.SelectedItems.Count == 0)
            {
                ptvm.SelectedNumOfParts = 0;
                ptvm.SelectedParts.Clear();
                return;
            }
            ptvm.SelectedParts.Clear();
            foreach (var item in partsListBox.SelectedItems)
            {
                ptvm.SelectedParts.Add((Part)item);
            }
            ptvm.SelectedNumOfParts = ptvm.SelectedParts.Count;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PartsOfTrayViewModel ptvm = (DataContext as PartsOfTrayViewModel);
            if (ptvm.SelectedNumOfPartsInTray > ptvm.Parts.Count)
            {
                return;
            }

            for (int i = 0; i < ptvm.SelectedNumOfPartsInTray; i++)
            {
                partsListBox.SelectedItems.Add(ptvm.Parts[i]);
            }
        }
    }
}
