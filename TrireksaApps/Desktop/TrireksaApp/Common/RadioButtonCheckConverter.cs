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



    public class RadioButtonPortTypeCheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PortType portType = (PortType)value;

            if (portType == PortType.Sea && parameter.ToString() == "Sea")
                return true;
            if (portType == PortType.Air&& parameter.ToString() == "Air")
                return true;
            if (portType == PortType.Land && parameter.ToString() == "Land")
                return true;
            if (portType == PortType.None && parameter.ToString() == "None")
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }









}
