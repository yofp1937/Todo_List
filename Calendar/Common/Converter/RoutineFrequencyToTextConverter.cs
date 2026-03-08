/*
 * ListView에서 Frequency를 Text로 변환해서 표시해주기위한 Converter
 */

using Calendar.Model.Enum;
using System.Globalization;
using System.Windows.Data;

namespace Calendar.Common.Converter
{
    public class RoutineFrequencyToTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // values[0] = Frequency (int), values[1] = RoutineType (Enum)
            if (values[0] is int freq && values[1] is RoutineType type)
            {
                string unit = type switch
                {
                    RoutineType.Daily => "일",
                    RoutineType.Weekly => "주",
                    RoutineType.Monthly => "개월",
                    RoutineType.Yearly => "년",
                    _ => ""
                };

                if (freq == 1)
                {
                    return type == RoutineType.Monthly ? "매달" : $"매{unit}";
                }
                return $"{freq}{unit}마다";
            }
            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
