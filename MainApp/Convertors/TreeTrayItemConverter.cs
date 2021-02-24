using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MainApp.Convertors
{
    public class TreeTrayItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;
            string result = "空槽";
            TrayStatus ts = (TrayStatus)values[0];
            if (ts != TrayStatus.TS_Empty)
            {
                result = string.Format("槽位{0}: 料盘类别:{1},料盘ID:{2}", values[1].ToString(), values[2].ToString(), values[3].ToString());
            }
            else
            {
                result = string.Format("空闲槽位{0}", values[1].ToString());
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
