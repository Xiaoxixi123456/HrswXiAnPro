using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MainApp.Convertors
{
    public class PartLedMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;
            PartStatus status = (PartStatus)values[0];
            if (status == PartStatus.PS_Measuring)
            {
                int Flag = (int)values[1];
                if (Flag % 2 == 0)
                    return new SolidColorBrush(Colors.Yellow);
                else
                    return new SolidColorBrush(Colors.Red);
            }
            if (status == PartStatus.PS_Measured)
            {
                bool pass = (bool)values[2];
                if (pass)
                    return new SolidColorBrush(Colors.Green);
                else
                    return new SolidColorBrush(Colors.Red);
            }
            if (status == PartStatus.PS_Error)
            {
                return new SolidColorBrush(Colors.Red);
            }
            if (status == PartStatus.PS_Placed)
            {
                return new SolidColorBrush(Colors.LightBlue);
            }
            if (status == PartStatus.PS_Idle)
            {
                return new SolidColorBrush(Colors.LightBlue);
            }
            return new SolidColorBrush(Colors.Gray); ;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
