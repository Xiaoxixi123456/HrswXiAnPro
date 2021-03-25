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
    public class TrayTreeViewBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TrayStatus ts = (TrayStatus)value;
            string color;
            switch (ts)
            {
                case TrayStatus.TS_Loading:
                    color = "LightBlue";
                    break;
                case TrayStatus.TS_Measuring:
                    color = "LightBlue";
                    break;
                case TrayStatus.TS_Measured:
                    color = "LightGreen";
                    break;
                case TrayStatus.TS_Unloading:
                    color = "LightBlue";
                    break;
                case TrayStatus.TS_Error:
                    color = "Red";
                    break;
                default:
                    color = "Transparent";
                    break;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
