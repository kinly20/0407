using System.Globalization;
using System.Windows.Data;

namespace Engine.Transfer
{
    public class ValueConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
           // if (DesignerProperties.GetIsInDesignMode(Application.Current.MainWindow))
             //   return true;
            if (values.All(v => v is bool))
            {
                bool value1 = (bool)values[0];
                bool value2 = (bool)values[1];
                if (!value1 && !value2)
                    return false;
                else
                    return true;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
