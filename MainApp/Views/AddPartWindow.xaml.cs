using Hrsw.XiAnPro.Models;
using MainApp.ViewModels;
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
    /// AddPartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddPartWindow : Window
    {
        public PartViewModel PartVM { get; set; }
        public ObservableCollection<Part> Parts { get; set; }
        public AddPartWindow()
        {
            InitializeComponent();
            PartVM = new PartViewModel();
            DataContext = PartVM;
        }

        private void AddPartHandler(object sender, RoutedEventArgs e)
        {
            if (radioButton.IsChecked == true)
            {
                if (Parts.Where(p => p.Category == PartVM.Category && p.Id == PartVM.DefId && string.Compare(p.Name, PartVM.PartName, true) == 0).Count() != 0)
                {
                    return;
                }
                Part part = new Part();
                part.Category = PartVM.Category;
                part.Name = PartVM.PartName;
                part.Id = PartVM.DefId;
                part.CmmNo = PartVM.CmmNo;
                part.Status = PartStatus.PS_Idle;
                part.TrayId = -1;
                part.SlotNb = -1;
                part.Pass = false;
                Parts.Add(part);
            }
            else if (radioButton1.IsChecked == true)
            {
                for (int i = 0; i < PartVM.Count; i++)
                {
                    int id = PartVM.StartId + i;
                    if (Parts.Where(p => p.Category == PartVM.Category && p.Id == id && string.Compare(p.Name, PartVM.PartName, true) == 0).Count() != 0)
                    {
                        continue;
                    }
                    Part part = new Part();
                    part.Category = PartVM.Category;
                    part.Name = PartVM.PartName;
                    part.Id = id;
                    part.CmmNo = PartVM.CmmNo;
                    part.Status = PartStatus.PS_Idle;
                    part.TrayId = -1;
                    part.SlotNb = -1;
                    part.Pass = false;
                    Parts.Add(part);
                }
            }
            else
            {
            }
        }

        private void CloseHandler(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
