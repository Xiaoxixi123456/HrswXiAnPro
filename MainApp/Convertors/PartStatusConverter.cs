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
    public class PartStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PartStatus ps = (PartStatus)value;
            string result = "";
            switch (ps)
            {
                case PartStatus.PS_Empty:
                    break;
                case PartStatus.PS_Idle:
                    result = "空闲";
                    break;
                case PartStatus.PS_Placed:
                    result = "已装夹";
                    break;
                case PartStatus.PS_Wait:
                    result = "等待测量";
                    break;
                case PartStatus.PS_Measuring:
                    result = "测量中";
                    break;
                case PartStatus.PS_Measured:
                    result = "测量完成";
                    break;
                case PartStatus.PS_Error:
                    result = "错误";
                    break;
                default:
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
