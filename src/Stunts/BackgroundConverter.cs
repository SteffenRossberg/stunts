using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace Stunts
{
    class BackgroundConverter<TItem> : IMultiValueConverter where TItem : BaseItem
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is ObservableCollection<ObservableCollection<TItem>> workspace))
                return Binding.DoNothing;
            if (!(values[1] is int row && row >= 0))
                return Binding.DoNothing;
            if (!(values[2] is int column && column >= 0))
                return Binding.DoNothing;
            return values[4];
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}