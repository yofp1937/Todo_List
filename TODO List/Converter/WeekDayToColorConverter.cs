/*
 * 요일별로 색깔 바꿔주는 클래스
 */
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TODO_List.Converter
{
    class WeekDayToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? day = value as string;
            return day switch
            {
                "Sun" => Brushes.Crimson,
                "Sat" => Brushes.Blue,
                _ => Brushes.Black,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
