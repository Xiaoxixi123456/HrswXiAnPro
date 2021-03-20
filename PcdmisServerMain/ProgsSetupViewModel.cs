using Hrsw.XiAnPro.ServerCommonMod;
using Hrsw.XiAnPro.Utilities;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PcdmisServerMain
{
    public class ProgsSetupViewModel : BindableBase
    {
        [Bindable]
        public ObservableCollection<MeasProg> MeasProgs { get; set; }
        [Bindable]
        public MeasProg SelectedMeasProg { get; set; }
        [Bindable]
        public string ProgramFileName { get; set; }
        [Bindable]
        public int Id { get; set; }


        public DelegateCommand SetupProgFileCommand { get; set; }
        public DelegateCommand AddProgFileCommand { get; set; }
        public DelegateCommand DeleteProgFileCommand { get; set; }
        public DelegateCommand FindFileNameCommand { get; set; }

        public ProgsSetupViewModel()
        {
            SetupProgFileCommand = new DelegateCommand(SetupProgFile);
            AddProgFileCommand = new DelegateCommand(AddProgFile);
            DeleteProgFileCommand = new DelegateCommand(DeleteProgFile);
            FindFileNameCommand = new DelegateCommand(FindFileName);
        }

        private void FindFileName()
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Filter = "Prg file (*.prg)|*.prg";
            ofdlg.Multiselect = false;
            if (ofdlg.ShowDialog().Value)
            {
                ProgramFileName = ofdlg.FileName;
            }
        }

        private void AddProgFile()
        {
            var count = MeasProgs.Where(p => p.Id == Id).Count();
            if (count > 0)
            {
                MessageBox.Show("ID重复", "警告", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            MeasProgs.Add(new MeasProg() { Id = Id, FileName = ProgramFileName });
            
        }

        private void DeleteProgFile()
        {
            if (SelectedMeasProg == null)
                return;
            MeasProgs.Remove(SelectedMeasProg);
        }

        private void SetupProgFile()
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Filter = "Prg file (*.prg)|*.prg";
            ofdlg.Multiselect = false;
            if (ofdlg.ShowDialog().Value)
            {
                SelectedMeasProg.FileName = ofdlg.FileName;
            }
        }
    }
}
