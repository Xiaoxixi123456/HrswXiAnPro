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
    public class TreePartItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;
            string result = "空槽";
            PartStatus ts = (PartStatus)values[0];
            if (ts != PartStatus.PS_Empty)
            {
                result = string.Format("槽位{0}: 工件名-{1},工件号-{2}", values[1].ToString(), values[2].ToString(), values[3].ToString());
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
