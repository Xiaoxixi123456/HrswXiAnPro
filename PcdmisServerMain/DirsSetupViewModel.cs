using Hrsw.XiAnPro.ServerCommonMod;
using Hrsw.XiAnPro.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PcdmisServerMain
{
    public class DirsSetupViewModel : BindableBase
    {
        [Bindable]
        public ServerDirManager Dirs { get; set; }

        public DelegateCommand SelectProgDirCommand { get; set; }
        public DelegateCommand SelectReportDirCommand { get; set; }
        public DelegateCommand SelectSafeLocateCommand { get; set; }

        public DirsSetupViewModel()
        {
            SelectProgDirCommand = new DelegateCommand(SelectProgDir);
            SelectReportDirCommand = new DelegateCommand(SelectReportDir);
            SelectSafeLocateCommand = new DelegateCommand(SelectSafeLocateProgram);

            Dirs = ServerDirManager.Inst;
            Dirs.LoadDirs();
        }

        private void SelectSafeLocateProgram()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "程序文件(*.Prg)|*.prg";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dirs.SafeLocateProgram = dlg.FileName;
            }
        }

        private void SelectReportDir()
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                Dirs.ResultDirectory = fbDlg.SelectedPath;
                Dirs.SaveDirs();
            }
        }

        private void SelectProgDir()
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                Dirs.MeasureProgDirectory = fbDlg.SelectedPath;
                Dirs.SaveDirs();
            }
        }
    }
}
