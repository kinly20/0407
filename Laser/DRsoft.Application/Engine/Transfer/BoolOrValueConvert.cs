using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Engine.Transfer
{
    public class BoolOrValueConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.All(v => v is bool))
            {
                bool value1 = (bool)values[0];
                bool value2 = (bool)values[1];
                if (value1 || value2)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
