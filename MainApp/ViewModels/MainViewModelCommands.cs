using Hrsw.XiAnPro.Models;
using MainApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ViewModels
{
    public partial class MainViewModel : BindableBase
    {
        public DelegateCommand SelectCategoryCommand { get; set; }
        public DelegateCommand AddPartCommand { get; set; }

        public void CreateCommands()
        {
            SelectCategoryCommand = new DelegateCommand(PartsUISelectCategory);
            AddPartCommand = new DelegateCommand(AddPart);
        }

        private void AddPart()
        {
            AddPartWindow addPartWindow = new AddPartWindow();
            addPartWindow.Parts = Parts;
            addPartWindow.ShowDialog();
            CategoriesRefresh();
        }

        private void PartsUISelectCategory()
        {
            if (CurrentSelectCategory != "All")
            {
                int result;
                bool ok = int.TryParse(CurrentSelectCategory, out result);
                if (!ok)
                    return;
                var ps = Parts.Where(p => p.Category == result);
                SelectParts = new ObservableCollection<Part>(ps);
            }
            else
            {
                SelectParts = Parts;
            }
        }
    }
}
