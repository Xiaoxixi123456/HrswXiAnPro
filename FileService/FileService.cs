using Contracts;
using Hrsw.XiAnPro.PCDmisService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileServices
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, UseSynchronizationContext = false)]
    public class FileService : IFileService
    {
        public UpFileResult UpLoadFile(UpFile filedata)
        {

            UpFileResult result = new UpFileResult();

            string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\service\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            byte[] buffer = new byte[filedata.FileSize];

            FileStream fs = new FileStream(path + filedata.FileName, FileMode.Create, FileAccess.Write);

            int count = 0;
            while ((count = filedata.FileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fs.Write(buffer, 0, count);
            }
            //清空缓冲区
            fs.Flush();
            //关闭流
            fs.Close();

            result.IsSuccess = true;

            return result;

        }

        //下载文件
        public DownFileResult DownLoadFile(DownFile filedata)
        {

            DownFileResult result = new DownFileResult();

            //string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\service\" + filedata.FileName;
            string path = Path.Combine(ServerDirManager.Inst.ResultProgDirectory, filedata.FileName);

            if (!File.Exists(path))
            {
                result.IsSuccess = false;
                result.FileSize = 0;
                result.Message = "服务器不存在此文件";
                result.FileStream = new MemoryStream();
                return result;
            }
            Stream ms = new MemoryStream();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            fs.CopyTo(ms);
            ms.Position = 0;  //重要，不为0的话，客户端读取有问题
            result.IsSuccess = true;
            result.FileSize = ms.Length;
            result.FileStream = ms;

            fs.Flush();
            fs.Close();
            return result;
        }
    }
}
