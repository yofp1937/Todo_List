/*
 * ListWindow의 ListView에서 Status를 문자열로 보여주기위해 값을 교환해주는 Converter
 */
using Calendar.Model.Enum;
using System.Globalization;
using System.Windows.Data;

namespace Calendar.Common.Converter
{
    public class StatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TodoStatus status)
            {
                return status switch
                {
                    TodoStatus.Waiting => "대기",
                    TodoStatus.Completion => "완료",
                    TodoStatus.Failure => "실패",
                    _ => status.ToString()
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
