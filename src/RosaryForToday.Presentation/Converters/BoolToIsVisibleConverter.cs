using System.Globalization;

namespace RosaryForToday.Presentation.Converters
{
    public class BoolToIsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                var invert = parameter is string s && string.Equals(s, "Invert", StringComparison.OrdinalIgnoreCase);
                return invert ? !b : b;
            }

            // fallback - not visible
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
