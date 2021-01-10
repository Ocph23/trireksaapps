using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TrireksaApp.Common
{
    public class CustomerAddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Customer data = (Customer)value;
            if (data != null)
                return string.Format("{0}\r{1}\rTlp:{2}/{3}/{4}", data.Address ?? "", data.City == null ? "" : data.City.CityName, data.Phone1 ?? "", data.Phone2 ?? "", data.Handphone ?? "");
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }

}
