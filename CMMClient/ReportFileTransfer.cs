﻿using Hrsw.XiAnPro.CMMClients.FileServiceReference;
using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.CMMClients
{
    public class ReportFileTransfer : Transfer<Part>
    {
        private FileServiceClient _fileServiceClient;
        private string _cmm;
        // TODO 文件传输事件

        public ReportFileTransfer(string cmm)
        {
            _blockQuene = new BlockingCollection<Part>();
            _cmm = cmm;
        }

        /// <summary>
        /// 连接失败会抛出异常
        /// </summary>
        public void Initialize()
        {
            _fileServiceClient = new FileServiceClient();
            _fileServiceClient.InnerChannel.Opened += InnerChannel_Opened;
            _fileServiceClient.InnerChannel.Closed += InnerChannel_Closed;
            _fileServiceClient.InnerChannel.Faulted += InnerChannel_Faulted;
            _fileServiceClient.Open();
        }

        private void InnerChannel_Faulted(object sender, EventArgs e)
        {
            
        }

        private void InnerChannel_Closed(object sender, EventArgs e)
        {
            _fileServiceClient.InnerChannel.Opened += InnerChannel_Opened;
            _fileServiceClient.InnerChannel.Closed += InnerChannel_Closed;
            _fileServiceClient.InnerChannel.Faulted += InnerChannel_Faulted;
            _fileServiceClient = null;
        }

        private void InnerChannel_Opened(object sender, EventArgs e)
        {
            
        }

        public override void LaunchTransferProcess()
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var part in _blockQuene.GetConsumingEnumerable())
                {
                    TransferFileFromCmmServer(part);
                }
            }, TaskCreationOptions.LongRunning);
        }

        private /*async */void TransferFileFromCmmServer(Part part)
        {
            //var result = await _fileServiceClient.DownLoadFileAsync(part.ResultFile);
            Task.Run(() =>
            {
                bool success;
                string message;
                Stream filestream = new MemoryStream();
                long fileSize = _fileServiceClient.DownLoadFile(part.ResultFile, out success, out message, out filestream);
                if (success)
                {
                    // TODO 目录处理
                    try
                    {
                        string root = _cmm == "pcdmis" ? ClientDirsManager.PcdmisReportsDirectory : ClientDirsManager.CalypsoReportsDirectory;
                        string filePath = Path.Combine(root, Path.GetFileName(part.ResultFile));
                        byte[] buffer = new byte[fileSize];
                        using (var fsm = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            //BinaryWriter bw = new BinaryWriter(fsm);
                            //bw.Write(((MemoryStream)filestream).ToArray());
                            int count = 0;
                            while ((count = filestream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fsm.Write(buffer, 0, count);
                            }
                            fsm.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                        // TODO 下载文件出错
                        //throw;
                    }
                }
                filestream.Close();
            });

        }

        internal void StopFileTransferService()
        {
            try
            {
                if (_fileServiceClient.State == CommunicationState.Opened)
                {
                    _fileServiceClient.Close();
                }
                else if (_fileServiceClient.State == CommunicationState.Faulted)
                {
                    _fileServiceClient.Abort();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
