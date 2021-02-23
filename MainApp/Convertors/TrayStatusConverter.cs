using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MainApp.Convertors
{
    public class TrayStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TrayStatus ts = (TrayStatus)value;
            string status = "";
            switch (ts)
            {
                case TrayStatus.TS_Idle:
                    status = "空闲";
                    break;
                case TrayStatus.TS_Placed:
                    status = "放置";
                    break;
                case TrayStatus.TS_Wait:
                    status = "等待测量";
                    break;
                case TrayStatus.TS_Loading:
                    status = "上料中";
                    break;
                case TrayStatus.TS_Measuring:
                    status = "测量中";
                    break;
                case TrayStatus.TS_Measured:
                    status = "测量完成";
                    break;
                case TrayStatus.TS_Unloading:
                    status = "下料中";
                    break;
                case TrayStatus.TS_Error:
                    status = "错误";
                    break;
                default:
                    status = "";
                    break;
            }
            return status;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
