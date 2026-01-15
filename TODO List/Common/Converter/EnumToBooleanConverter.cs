/*
 * RadioButton의 체크 여부(Boolean)와 Enum값을 서로 변환해주는 컨버터
 */
using System.Globalization;
using System.Windows.Data;

namespace TODO_List.Common.Converter
{
    public class EnumToBooleanConverter : IValueConverter
    {
        // ViewModel -> View (라디오 버튼 체크 상태 결정)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }

        // View -> ViewModel (사용자가 체크한 라디오 버튼의 Enum 값 전달)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                return Enum.Parse(targetType, parameter.ToString());
            }
            return Binding.DoNothing;
        }
    }
}
