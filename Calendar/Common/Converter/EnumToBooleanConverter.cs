/*
 * View와 ViewModel의 통신에서 Binding된 Enum 값이 Parameter로 들어온 값과 같은지 확인해주는 컨버터
 * 사용처: SidePanelTasksView, TodoWindow, RoutineView
 */
using Calendar.Model.Enum;
using System.Globalization;
using System.Windows.Data;

namespace Calendar.Common.Converter
{
    public class EnumToBooleanConverter : IValueConverter
    {
        // ViewModel(Enum) -> View(bool)
        // 현재 Status값이 파라미터로 들어온 값과 같으면 true(체크) 반환
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;

            string? checkValue = value.ToString();
            string? targetValue = parameter.ToString();

            if (checkValue == null || targetValue == null) return false;

            return checkValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase);
        }

        // View(bool) -> ViewModel(Enum)
        // 사용자가 라디오 버튼을 체크하면 파라미터의 문자열을 Enum으로 변환해서 전달
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked)
            {
                if (isChecked && parameter != null) // 체크됨 -> Completion
                {
                    return Enum.Parse(targetType, parameter.ToString() ?? "Completion");
                }
                else // 체크 해제됨 -> Waiting
                {
                    return TodoStatus.Waiting;
                }
            }
            return Binding.DoNothing;
        }
    }
}