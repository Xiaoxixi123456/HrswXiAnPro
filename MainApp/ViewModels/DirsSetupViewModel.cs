using Hrsw.XiAnPro.CMMClients;
using Hrsw.XiAnPro.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp.ViewModels
{
    public class DirsSetupViewModel : BindableBase
    {
        [Bindable]
        public ClientDirsManager Dirs { get; set; }

        public DelegateCommand SelectedPcdDirCommand { get; set; }
        public DelegateCommand SelectedCalpDirCommand { get; set; }

        public DirsSetupViewModel()
        {
            ClientDirsManager.Inst.LoadDirs();
            Dirs = ClientDirsManager.Inst;

            SelectedPcdDirCommand = new DelegateCommand(SelectedPcdDirectory);
            SelectedCalpDirCommand = new DelegateCommand(SelectedCalpDirectory);
        }

        private void SelectedCalpDirectory()
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                Dirs.CalypsoReportsDirectory = fbDlg.SelectedPath;
                Dirs.SaveDirs();
            }
        }

        private void SelectedPcdDirectory()
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                Dirs.PcdmisReportsDirectory = fbDlg.SelectedPath;
                Dirs.SaveDirs();
            }
        }
    }
}
