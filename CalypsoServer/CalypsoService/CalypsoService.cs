using CalypsoServiceInterfaces;
using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.ServerCommonMod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalypsoServices
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CalypsoService : ICalypsoService
    {
        public bool MakeOffsetFile(CalypsoRequest request)
        {
            bool result = true;
            try
            {
                GenerateMeasureParameterFile(request.Part);
            }
            catch (Exception)
            {
                //记录错误
                result = false;
            }
            return result;
        }

        private void GenerateMeasureParameterFile(Part part)
        {
            //Calypso可能会变动路径
            string fileName = Path.Combine(ServerDirManager.Inst.MeasureProgDirectory, "MeasParams.Par");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"xoff={part.XOffset}");
            sb.AppendLine($"yoff={part.YOffset}");
            File.WriteAllText(fileName, sb.ToString());
        }

        public string GetReportFilename()
        {
            try
            {
                string fileName;
                fileName = Path.Combine(ServerDirManager.Inst.MeasureProgDirectory, "report.rpt");
                string[] fileNames = File.ReadAllLines(fileName);
                if (fileNames.Length == 0)
                    return "";
                return fileNames[0];
            }
            catch (Exception ex)
            {
                ServerLog.Logs.AddLog(ex.Message);
                return "";
            }
        }
    }
}
