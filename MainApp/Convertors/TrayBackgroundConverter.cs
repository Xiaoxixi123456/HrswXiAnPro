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
    public class TrayBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TrayStatus ts = (TrayStatus)value;
            SolidColorBrush result = null;
            switch (ts)
            {
                case TrayStatus.TS_Empty:
                    result = new SolidColorBrush(Colors.Gray);
                    break;
                case TrayStatus.TS_Idle:
                    result = new SolidColorBrush(Colors.LightYellow);
                    break;
                case TrayStatus.TS_Wait:
                    result = new SolidColorBrush(Colors.AliceBlue);
                    break;
                case TrayStatus.TS_Loading:
                    result = new SolidColorBrush(Colors.LightBlue);
                    break;
                case TrayStatus.TS_Measuring:
                    result = new SolidColorBrush(Colors.LightBlue);
                    break;
                case TrayStatus.TS_Measured:
                    result = new SolidColorBrush(Colors.Blue);
                    break;
                case TrayStatus.TS_Unloading:
                    result = new SolidColorBrush(Colors.LightBlue);
                    break;
                case TrayStatus.TS_Error:
                    result = new SolidColorBrush(Colors.Red);
                    break;
                case TrayStatus.TS_Placed:
                    result = new SolidColorBrush(Colors.LightYellow);
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
