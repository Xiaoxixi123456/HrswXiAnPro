using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MainApp.Convertors
{
    public class CmmIndexToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)value;
            string cmmName;
            switch (index)
            {
                case 0:
                    cmmName = "Pcdmis";
                    break;
                case 1:
                    cmmName = "Calypso";
                    break;
                case 2:
                    cmmName = "AnyCmm";
                    break;
                default:
                    cmmName = "";
                    break;
            }
            return cmmName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
