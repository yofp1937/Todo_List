/*
 * ConverterParameter로 넘겨받은 값(기본 10)보다 Index가 커지면 Visibility를 Collapsed로 바꿔주는 Converter
 * 사용처: Calendar
 */
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Calendar.Common.Converter
{
    public class ComparisonToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 1. 바인딩된 값(현재 인덱스) 확인
            if (value is int index)
            {
                // 2. 파라미터로 넘어온 제한 숫자 확인 (XAML에서 넘겨준 값)
                // 파라미터가 없으면 기본값 10 사용
                int limit = 10;
                if (parameter != null && int.TryParse(parameter.ToString(), out int p))
                {
                    limit = p;
                }

                // 3. 비교 로직 수행 (limit번째부터는 숨김)
                return index >= limit ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
