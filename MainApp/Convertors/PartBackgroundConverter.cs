using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MainApp.Convertors
{
    public class PartBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PartStatus ps = (PartStatus)value;
            SolidColorBrush result = null;
            switch (ps)
            {
                case PartStatus.PS_Empty:
                    result = new SolidColorBrush(Colors.Gray);
                    break;
                case PartStatus.PS_Idle:
                    result = new SolidColorBrush(Colors.Cornsilk);
                    break;
                case PartStatus.PS_Wait:
                    result = new SolidColorBrush(Colors.Blue);
                    break;
                case PartStatus.PS_Measuring:
                    result = new SolidColorBrush(Colors.Yellow);
                    break;
                case PartStatus.PS_Measured:
                    result = new SolidColorBrush(Colors.Green);
                    break;
                case PartStatus.PS_Error:
                    result = new SolidColorBrush(Colors.Red);
                    break;
                default:
                    result = new SolidColorBrush(Colors.Gray);
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
