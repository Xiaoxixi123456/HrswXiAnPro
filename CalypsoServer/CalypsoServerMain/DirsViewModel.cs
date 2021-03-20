using CalypsoServices;
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

namespace CalypsoServerMain
{
    public class DirsViewModel : BindableBase
    {
        [Bindable]
        public ServerDirManager Dirs { get; set; }

        public DelegateCommand SelectReportDirCommand { get; set; }
        public DelegateCommand SelectMeasureProgDirCommand { get; set; }

        public DirsViewModel()
        {
            ServerDirManager.Inst.LoadDirs();
            Dirs = ServerDirManager.Inst;

            SelectReportDirCommand = new DelegateCommand(SelectReportDir);
            SelectMeasureProgDirCommand = new DelegateCommand(SelectMeasureProgDir);
        }

        private void SelectMeasureProgDir()
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                Dirs.MeasureProgDirectory = fbDlg.SelectedPath;
                Dirs.SaveDirs();
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
    }
}
