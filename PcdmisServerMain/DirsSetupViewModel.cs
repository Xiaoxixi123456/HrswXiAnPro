﻿using Hrsw.XiAnPro.PCDmisService;
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

        public DirsSetupViewModel()
        {
            SelectProgDirCommand = new DelegateCommand(SelectProgDir);
            SelectReportDirCommand = new DelegateCommand(SelectReportDir);

            Dirs = ServerDirManager.Inst;
            Dirs.LoadDirs();
        }

        private void SelectReportDir()
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            if (fbDlg.ShowDialog() == DialogResult.OK)
            {
                Dirs.ResultProgDirectory = fbDlg.SelectedPath;
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
