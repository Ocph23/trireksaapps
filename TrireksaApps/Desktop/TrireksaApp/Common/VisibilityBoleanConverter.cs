using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TrireksaApp.Common
{
    public class VisibilityBoleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool isTrue = (bool)value;
            if (isTrue)
                return Visibility.Visible;
            else
            {
                Int32.TryParse(parameter.ToString(), out int paramValue);
                if (paramValue > 0)
                    return Visibility.Collapsed;
                return Visibility.Hidden;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
