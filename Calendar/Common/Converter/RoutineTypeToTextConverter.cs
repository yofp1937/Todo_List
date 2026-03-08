/*
 * ListView에서 RoutineType에따라 일간, 주간, 월간, 연간 반복 형태를 Text로 표시해주기위한 Converter
 */
using Calendar.Model.Enum;
using System.Globalization;
using System.Windows.Data;

namespace Calendar.Common.Converter
{
    public class RoutineTypeToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RoutineType type)
            {
                return type switch
                {
                    RoutineType.Daily => "일간",
                    RoutineType.Weekly => "주간",
                    RoutineType.Monthly => "월간",
                    RoutineType.Yearly => "연간",
                    _ => "없음"
                };
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
