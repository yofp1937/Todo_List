/*
 * WeeklyRoutine에서 사용하는 체크박스 설정용 ViewModel
 */
using TODO_List.Model;

namespace TODO_List.ViewModel.Routine
{
    public class WeeklyDayOptionViewModel : BaseViewModel
    {
        public string DayName { get; set; } = string.Empty; // 화면 표시용 (일, 월...)
        public DayOfWeek DayValue { get; set; }           // 시스템 계산용 (0~6)
        public string TextColor { get; set; } = "Black";    // 글자 색상

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
