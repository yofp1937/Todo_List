/*
 * Calendar에서 일정, 규칙이 10개를 넘으면 9번째 TextBlock을 ...(+n)으로 표시하려하는데 해당 기능을 수행하는 Converter
 * 사용처: Calendar
 */
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Calendar.Common.Converter
{
    public class TaskSummaryTextConverter : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == DependencyProperty.UnsetValue) return "";

            // 1. Binding된 값의 전체 갯수 확인
            int totalCount = 0;
            if (values is IEnumerable enumerable)
                totalCount = enumerable.Cast<object>().Count();

            // 2. 파라미터로 넘어온 제한 갯수 확인
            int limitCount = 7;
            if (parameter != null && int.TryParse(parameter.ToString(), out int p))
            {
                limitCount = p;
            }

            // 3. 데이터가 제한개수 이상이면 요약 테스트 반환
            if (totalCount >= limitCount)
            {
                // 전체 갯수 - 표시해준 갯수
                return $"... (+{totalCount - (limitCount - 1)})";
            }

            return "";
        }
        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
