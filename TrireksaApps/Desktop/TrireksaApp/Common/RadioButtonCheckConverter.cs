using ModelsShared.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TrireksaApp.Common
{

   


    public class RadioButtonCheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true)?parameter : Binding.DoNothing;
        }
    }

    

   


    


}
