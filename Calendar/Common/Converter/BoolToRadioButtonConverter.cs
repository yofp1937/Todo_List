/*
 * bool 프로퍼티와 RadioButton의 IsChecked 상태를 상호 변환해주는 컨버터
 */
using System.Globalization;
using System.Windows.Data;

namespace Calendar.Common.Converter
{
    public class BoolToRadioButtonConverter : IValueConverter
    {
        // ViewModel -> View (데이터 상태를 화면에 반영하는 부분)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;

            bool targetValue = bool.Parse(parameter.ToString()!);
            return value.Equals(targetValue);
        }

        // View -> ViewModel (사용자가 클릭했을 때 값을 저장하는 부분)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return Binding.DoNothing;

            // 라디오 버튼이 체크된 상태(true)일 때만 해당 파라미터 값을 프로퍼티에 전달
            if ((bool)value)
            {
                return bool.Parse(parameter.ToString()!);
            }

            return Binding.DoNothing;
        }
    }
}
