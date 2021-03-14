using System.Globalization;
using System.Windows.Data;

namespace Stunts
{
    class CurrentItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
            => values[0] != null && (Equals(values[0], values[1]) || Equals(values[0], values[2]));

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}