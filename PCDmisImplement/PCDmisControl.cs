using Microsoft.Win32;
using PCDLRN;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.PCDmisImplement
{
    public class PCDmisControl
    {
        private Application _pcdApplication = null;
        private PartPrograms _pcdPartPrograms = null;
        private ApplicationObjectEvents _pcdAppEvent = null;
        private PartProgram _pcdPartProgram = null;
        public bool IsExecuted { get; set; }
        private bool _isRunning;
        private bool _isPaused;

        public event EventHandler<PcdmisEventArgs> Executed;
        public event EventHandler<PcdmisEventArgs> ExecuteError;
        public event EventHandler<PcdmisEventArgs> PcdmisMessageBox;

        public void InitPcdmis()
        {
            try
            {
                ClosePCDmis();
                Type t = Type.GetTypeFromProgID("PCDLRN.Application");
                _pcdApplication = (Application)Activator.CreateInstance(t);
                bool ok = _pcdApplication.WaitUntilReady(60);
                if (!ok) throw new Exception("PCDMIS创建异常");
                //Thread.Sleep(5000);
                _pcdApplication.UserExit = true;
                _pcdPartPrograms = _pcdApplication.PartPrograms;
                _pcdAppEvent = _pcdApplication.ApplicationEvents;
                _pcdAppEvent.OnCloseExecutionDialog += OnExecuteEnd;
                _pcdAppEvent.OnPCDMessageBoxOpen += OnPCDMessageBoxOpen;
            }
            catch (Exception)
            {
                throw new PcdmisServiceException(messages[1]);
            }
        }

        private void OnExecuteEnd(ExecutionWindow ExecutionWindow)
        {
            IsExecuted = true;
            Executed?.Invoke(this, new PcdmisEventArgs(_pcdPartProgram.ExecutionWasCancelled, null));
        }

        private void OnExecuteErrorMsg(string ErrorMsg)
        {
            ExecuteError?.Invoke(this, new PcdmisEventArgs(ErrorMsg));
        }


        private List<string> mboxInfo = new List<string>
        {
            "请旋转到ID",
            "请加载测头"
        };

        private bool ContainMessageFound(string message)
        {
            foreach (string item in mboxInfo)
            {
                if (message.ToLower().Contains(item.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        private void OnPCDMessageBoxOpen(PCDMessageBox PCDMessageBox)
        {
            string message = PCDMessageBox.Message;
            if (ContainMessageFound(PCDMessageBox.Message))
            {
                CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
                Task.Run(() =>
                {
                    PCDLRN.ENUM_PRESS_BUTTON_RESULTS result = PCDLRN.ENUM_PRESS_BUTTON_RESULTS.DIALOG_NOT_FOUND;
                    do
                    {
                        if (PCDMessageBox.IsClosed)
                            break;
                        if (cts.IsCancellationRequested)
                            break;
                        if (PCDMessageBox.HasButton(PCDLRN.ENUM_BUTTON_TYPE.BUTTON_TYPE_OK))
                        {
                            result = PCDMessageBox.PressButton(PCDLRN.ENUM_BUTTON_TYPE.BUTTON_TYPE_OK);
                        }
                    } while (result != PCDLRN.ENUM_PRESS_BUTTON_RESULTS.SUCCESS);
                }, cts.Token);
                return;
            }
            PcdmisMessageBox?.Invoke(this, new PcdmisEventArgs(PCDMessageBox.Message));
        }

        public bool OpenProgram(string partProgramFile)
        {
            Debug.Assert(!string.IsNullOrEmpty(partProgramFile));
            Debug.Assert(_pcdPartPrograms != null);

            if (string.Compare(_pcdPartProgram.FullName, partProgramFile, true) == 0)
            {
                return true;
            }

            try
            {
                if (_pcdPartPrograms.Count > 0)
                {
                    bool ret = false;
                    for (int i = _pcdPartPrograms.Count; i > 0; i--)
                    {
                        ret = _pcdPartPrograms.Remove(i);
                    }
                    if (!ret)
                    {
                        return ret;
                    }
                    if (_pcdPartProgram != null)
                    {
                        Marshal.FinalReleaseComObject(_pcdPartProgram);
                        _pcdPartProgram = null;
                    }
                    GC.Collect();
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
                _pcdPartProgram = null;
                _pcdPartProgram = _pcdPartPrograms.Add(partProgramFile, PCDLRN.UNITTYPE.MM, _pcdApplication.DefaultMachineName, _pcdApplication.DefaultProbeFile);
                if (_pcdPartProgram != null)
                {
                    _pcdPartProgram.ClearAllTADs();
                    _pcdPartProgram.ClearExecutionBlock();
                    _pcdPartProgram.ClearExecutionMarkedSet();
                    _pcdPartProgram.ClearVerifyFeaturesFlag();
                    _pcdPartProgram.RefreshPart();
                }
                Thread.Sleep(1000); // 等待PCDMIS打开程序
                //_isOpened = _pcdPartProgram != null;
            }
            catch (Exception e)
            {
                throw new PcdmisServiceException(e.Message);
            }
            if (_pcdPartProgram == null)
                throw new PcdmisServiceException(messages[3]);
            return true;
        }



        public bool ExecuteProgramAsync()
        {
            Debug.Assert(_pcdPartProgram != null);
            _pcdApplication.Visible = true;
            // 运行状态
            try
            {
                _isRunning = _pcdPartProgram.AsyncExecute();
                if (!_isRunning)
                    throw new PcdmisServiceException(messages[5]);
                return _isRunning;
            }
            catch (Exception)
            {
                throw new PcdmisServiceException(messages[5]);
            }
        }

        public void PauseProgram()
        {
            //Debug.Assert(_pcdPartProgram != null);
            if (_pcdPartProgram == null) return;
            try
            {
                if (_isRunning && !_isPaused)
                {
                    _pcdPartProgram.GetExecutionWindow(0).stop();
                    _isPaused = true;
                }
            }
            catch (Exception)
            {
                throw new PcdmisServiceException(messages[7]);
            }
        }

        public void ContinueProgram()
        {
            //Debug.Assert(_pcdPartProgram != null);
            if (_pcdPartProgram == null) return;

            try
            {
                if (_isRunning && _isPaused)
                {
                    _pcdPartProgram.GetExecutionWindow(0).Continue();
                    _isPaused = false;
                }
            }
            catch (Exception)
            {
                throw new PcdmisServiceException(messages[9]);
            }
        }

        public void CancelProgram()
        {
            //Debug.Assert(_pcdPartProgram != null);
            if (_pcdPartProgram == null) return;
            try
            {
                if (_isRunning)
                {
                    _pcdPartProgram.GetExecutionWindow(0).CancelExecution();
                    _isRunning = false;
                }
            }
            catch (Exception)
            {
                throw new PcdmisServiceException(messages[11]);
            }
        }

        public void Close()
        {
            Debug.Assert(_pcdApplication != null);

            try
            {
                if (_pcdPartPrograms != null)
                {
                    _pcdPartPrograms.CloseAll();
                    //Marshal.FinalReleaseComObject(_pcdPartPrograms);
                }
                _pcdApplication.Quit();
                //Marshal.FinalReleaseComObject(_pcdApplication);
            }
            catch (Exception)
            {
            }
        }

        public IEnumerable<Dimension> GetDimensions()
        {
            Debug.Assert(_pcdPartProgram != null);

            List<Dimension> dimensions = new List<Dimension>();

            try
            {
                Commands cmds = _pcdPartProgram.Commands;
                int index = 1;
                while (true)
                {
                    if (index > cmds.Count) break;
                    Command cmd = cmds.Item(index);

                    // 获取组合尺寸（位置或者位置度）
                    if (cmd.Type == OBTYPE.DIMENSION_START_LOCATION ||
                        cmd.Type == OBTYPE.DIMENSION_TRUE_START_POSITION)
                    {
                        DimensionCmd dimCmd = cmd.DimensionCommand;
                        Dimension dim = new Dimension();
                        dim.ID = cmd.ID;
                        dim.Type = cmd.TypeDescription;
                        dim.Feat1 = dimCmd.Feat1;
                        dim.Feat2 = dimCmd.Feat2;
                        dim.Feat3 = dimCmd.Feat3;
                        dim.IsOutTol = false;
                        while (true)
                        {
                            ++index;
                            cmd = cmds.Item(index);
                            if (cmd.Type == OBTYPE.DIMENSION_END_LOCATION ||
                                cmd.Type == OBTYPE.DIMENSION_TRUE_END_POSITION) break;
                            Debug.Assert(cmd.IsDimension);
                            dimCmd = cmd.DimensionCommand;
                            DimensionItem dimItem = new DimensionItem();
                            dimItem.Axis = dimCmd.AxisLetter;
                            dimItem.Nominal = dimCmd.NOMINAL;
                            dimItem.Measured = dimCmd.Measured;
                            dimItem.Plus = dimCmd.Plus;
                            dimItem.Minus = dimCmd.Minus;
                            dimItem.Deviation = dimCmd.Deviation;
                            dimItem.OutTol = dimCmd.OutTol;
                            if (Math.Abs(dimItem.OutTol) > 1E-6)
                                dim.IsOutTol = true;
                            dim.DimensionData.Add(dimItem);
                        }

                        dimensions.Add(dim);
                    }
                    else if (cmd.IsDimension)
                    {
                        DimensionCmd dimCmd = cmd.DimensionCommand;
                        Dimension dim = new Dimension();
                        dim.ID = dimCmd.ID;
                        dim.Type = cmd.TypeDescription;
                        dim.Feat1 = dimCmd.Feat1;
                        dim.Feat2 = dimCmd.Feat2;
                        dim.Feat3 = dimCmd.Feat3;
                        dim.IsOutTol = false;
                        DimensionItem dimItem = new DimensionItem();
                        dimItem.Axis = dimCmd.AxisLetter;
                        dimItem.Nominal = dimCmd.NOMINAL;
                        dimItem.Measured = dimCmd.Measured;
                        dimItem.Plus = dimCmd.Plus;
                        dimItem.Minus = dimCmd.Minus;
                        dimItem.Deviation = dimCmd.Deviation;
                        dimItem.OutTol = dimCmd.OutTol;
                        if (Math.Abs(dimItem.OutTol) > 1E-6)
                            dim.IsOutTol = true;
                        dim.DimensionData.Add(dimItem);

                        dimensions.Add(dim);
                    }

                    ++index;
                }
            }
            catch (Exception)
            {
                throw new PcdmisServiceException(messages[12]);
            }

            return dimensions;
        }

        //public IEnumerable<object> GetDimensionsTest()
        //{
        //    Debug.Assert(_pcdPartProgram != null);

        //    List<object> dimData = new List<object>();

        //    Commands cmds = _pcdPartProgram.Commands;
        //    int count = cmds.Count;

        //    for (int index = 1; index <= count; index++)
        //    {
        //        Command cmd = cmds.Item(index);
        //        Debug.WriteLine($"cmd.Type: {cmd.Type}");
        //        Debug.WriteLine($"cmd类型描述: {cmd.TypeDescription}");
        //        Debug.WriteLine($"cmd.ID: {cmd.ID}");
        //        Debug.WriteLine("------------------------");
        //        if (cmd.IsDimension)
        //        {
        //            DimensionCmd dc = cmd.DimensionCommand;
        //            Debug.WriteLine($"dc.id: {dc.ID}");
        //            Debug.WriteLine($"元素1: {dc.Feat1}");
        //            Debug.WriteLine($"元素2: {dc.Feat2}");
        //            Debug.WriteLine($"元素3: {dc.Feat3}");
        //            Debug.WriteLine($"轴类型: {dc.AXIS}");
        //            Debug.WriteLine($"轴描述AxisLetter: {dc.AxisLetter}");
        //            Debug.WriteLine($"实际值: {dc.Measured}");
        //            Debug.WriteLine($"理论值: {dc.NOMINAL}");
        //            Debug.WriteLine($"上公差: {dc.Plus}");
        //            Debug.WriteLine($"下公差: {dc.Minus}");
        //            Debug.WriteLine($"误差值: {dc.Deviation}");
        //            Debug.WriteLine($"超差: {dc.OutTol}");
        //            Debug.WriteLine($"dc.Angle: {dc.Angle}");
        //            Debug.WriteLine($"dc.Angle2dSupplemental: {dc.Angle2DSupplemental}");
        //            Debug.WriteLine($"dc.Angle2DToFrom: {dc.Angle2DToFrom}");
        //            Debug.WriteLine("============================");
        //            Debug.WriteLine("");
        //        }
        //    }

        //    return dimData;
        //}

        private void Offline(Type t)
        {
            string clsid = "{" + t.GUID.ToString() + "}";
            RegistryKey clsKey = Registry.ClassesRoot.OpenSubKey(@"CLSID\" + clsid);
            if (clsKey == null) // 处理64位PCDMIS
            {
                RegistryKey classRoot = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);
                clsKey = classRoot.OpenSubKey(@"CLSID\" + clsid);
                classRoot.Close();
            }
            string[] valueNames = clsKey.GetSubKeyNames();
            List<string> nalist = new List<string>(valueNames);
            string path = "";
            nalist.ForEach(exe =>
            {
                if (string.Compare(exe, "localserver32", true) == 0)
                {
                    RegistryKey pathKey = clsKey.OpenSubKey(exe);
                    path = (string)pathKey.GetValue(pathKey.GetValueNames()[0]);
                    path = path.TrimStart('\"').TrimEnd('\"');
                    path = Path.GetDirectoryName(path);
                }
            });
            if (path != "")
            {
                string startFile = Path.Combine(path, "AutomationStartupOptions.txt");
                File.WriteAllText(startFile, "/f");
            }
        }

        private bool ClosePCDmis()
        {
            bool result = false;
            Process[] processes = Process.GetProcessesByName("PCDLRN");
            while (processes.Length > 0)
            {
                foreach (var item in processes)
                {
                    result = item.CloseMainWindow();
                    if (!result)
                    {
                        item.Kill();
                        result = item.WaitForExit((int)TimeSpan.FromSeconds(10).TotalMilliseconds);
                    }
                }
                processes = Process.GetProcessesByName("PCDLRN");
            }
            return result;
        }

        private static readonly Dictionary<int, string> messages = new Dictionary<int, string>()
        {
            {0, "Pcdmis初始化完成" },
            {1, "Pcdmis初始化异常" },
            {2, "打开零件程序文件成功" },
            {3, "打开零件程序文件异常" },
            {4, "开始执行零件程序" },
            {5, "执行零件程序异常" },
            {6, "暂停执行" },
            {7, "执行暂停任务异常" },
            {8, "继续零件程序" },
            {9, "继续零件程序任务异常" },
            {10, "取消执行零件程序" },
            {11, "取消执行零件程序异常" },
            {12, "获取尺寸数据异常" }
        };
    }
}

