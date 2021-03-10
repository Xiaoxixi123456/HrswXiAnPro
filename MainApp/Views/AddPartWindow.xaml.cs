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
        //public ObservableCollection<Part> SelectedParts { get; set; }
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
                if (Parts.Where(p => p.Category == PartVM.Category && p.PartNb == PartVM.DefId && string.Compare(p.Name, PartVM.PartName, true) == 0).Count() != 0)
                {
                    return;
                }
                Part part = new Part();
                part.Id = Parts.Count + 1;
                part.Category = PartVM.Category;
                part.Name = PartVM.PartName;
                part.PartNb = PartVM.DefId;
                part.CmmNo = PartVM.CmmNo;
                part.UseCmmNo = -1;
                part.Status = PartStatus.PS_Idle;
                part.TrayNb = -1;
                part.SlotNb = -1;
                part.Pass = false;
                part.Placed = false;
                Parts.Add(part);
                //SelectedParts.Add(part);
            }
            else if (radioButton1.IsChecked == true)
            {
                for (int i = 0; i < PartVM.Count; i++)
                {
                    int nb = PartVM.StartId + i;
                    if (Parts.Where(p => p.Category == PartVM.Category && p.PartNb == nb && string.Compare(p.Name, PartVM.PartName, true) == 0).Count() != 0)
                    {
                        continue;
                    }
                    Part part = new Part();
                    part.Id = Parts.Count + 1;
                    part.Category = PartVM.Category;
                    part.Name = PartVM.PartName;
                    part.PartNb = nb;
                    part.CmmNo = PartVM.CmmNo;
                    part.UseCmmNo = -1;
                    part.Status = PartStatus.PS_Idle;
                    part.TrayNb = -1;
                    part.SlotNb = -1;
                    part.Pass = false;
                    part.Placed = false;
                    Parts.Add(part);
                    //SelectedParts.Add(part);
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
