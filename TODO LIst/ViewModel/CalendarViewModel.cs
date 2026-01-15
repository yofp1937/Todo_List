/*
 * 메인 UI에 달력을 자동으로 생성해주는 클래스
 */
using System.Collections.ObjectModel;
using TODO_List.Model;
using TODO_List.Model.DataClass;

namespace TODO_List.ViewModel
{
    public class CalendarViewModel : BaseViewModel
    {
        #region Property
        public ObservableCollection<string> WeekDays { get; private set; } = new ObservableCollection<string>
        {
             "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"
        };
        public ObservableCollection<CalendarDay> Days { get; private set; } = new();

        private DateTime _currentMonth;
        public DateTime CurrentMonth
        {
            get => _currentMonth;
            set
            {
                // value가 기존 _currentMonth와 다르면 if문 실행
                if (SetProperty(ref _currentMonth, value))
                {
                    OnPropertyChanged(nameof(CurrentMonthText));
                    CreateCalendar(CurrentMonth);
                }
            }
        }
        public string CurrentMonthText => $"{_currentMonth.Year}년 {_currentMonth.Month}월";
        #endregion

        #region 생성자, override
        /// <summary>
        /// CalendarBuilder 생성자
        /// </summary>
        public CalendarViewModel()
        {
            CurrentMonth = DateTime.Today; // 2.CurrentMonth 설정으로 달력 동적 생성
        }
        #endregion

        #region 달력 생성
        /// <summary>
        /// targetMonth에 해당하는 달의 달력 생성
        /// </summary>
        /// <param name="targetMonth">생성을 원하는 달</param>
        public void CreateCalendar(DateTime targetMonth)
        {
            Days.Clear();

            DateTime firstDay = new DateTime(targetMonth.Year, targetMonth.Month, 1);
            int startOffset = (int)firstDay.DayOfWeek; // 0: 일요일 ~ 6: 토요일
            int daysInMonth = DateTime.DaysInMonth(targetMonth.Year, targetMonth.Month); // 이번달이 며칠인지

            // 이전달 공백
            AddPreviousMonthDays(targetMonth.AddMonths(-1), startOffset);

            // 이번달 날짜
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime date = new DateTime(CurrentMonth.Year, CurrentMonth.Month, day);
                Days.Add(new CalendarDay
                {
                    DayNumber = day,
                    DayOfWeek = date.DayOfWeek,
                    IsToday = date == DateTime.Today.Date,
                    IsHoliday = HolidayProvider.IsHoliday(date),
                    IsCurrentMonth = true,
                });
            }

            // 남은 칸은 다음달 공백
            int totalCells = ((Days.Count + 6) / 7) * 7;
            int remainingCells = totalCells - Days.Count;
            AddNextMonthDays(targetMonth.AddMonths(1), remainingCells);
        }

        /// <summary>
        /// 현재 표시되는 달력에서 이전달에 해당하는 부분의 날짜를 채워주는 함수
        /// </summary>
        /// <param name="targetMonth">이전달이 몇년 몇월인지 매개변수로 넣어야함</param>
        /// <param name="offset">이전달 날짜를 몇개 표시해야하는지 카운트</param>
        private void AddPreviousMonthDays(DateTime targetMonth, int offset)
        {
            if (offset == 0) return;

            int daysInMonth = DateTime.DaysInMonth(targetMonth.Year, targetMonth.Month);
            // daysInMonth = 30, offset이 2일 경우 29, 30의 숫자가 필요한데 +1이 없으면 28, 29가 들어감
            int startDay = daysInMonth + 1 - offset;

            for (int day = startDay; day <= daysInMonth; day++)
            {
                DateTime date = new DateTime(targetMonth.Year, targetMonth.Month, day); // 날짜 객체 생성
                Days.Add(new CalendarDay
                {
                    DayNumber = day,
                    DayOfWeek = date.DayOfWeek,
                    IsHoliday = HolidayProvider.IsHoliday(date),
                    IsCurrentMonth = false,
                });
            }
        }

        /// <summary>
        /// 현재 표시되는 달력에서 다음달에 해당하는 부분의 날짜를 채워주는 함수
        /// </summary>
        /// <param name="targetMonth">다음달이 몇년 몇월인지 매개변수로 넣어야함</param>
        /// <param name="offset">다음달 날짜를 몇개 표시해야하는지 카운트</param>
        private void AddNextMonthDays(DateTime targetMonth, int offset)
        {
            if (offset == 0) return;

            int daysInMonth = DateTime.DaysInMonth(targetMonth.Year, targetMonth.Month);

            for (int day = 1; day <= offset; day++)
            {
                DateTime date = new DateTime(targetMonth.Year, targetMonth.Month, day); // 날짜 객체 생성
                Days.Add(new CalendarDay
                {
                    DayNumber = day,
                    DayOfWeek = date.DayOfWeek,
                    IsHoliday = HolidayProvider.IsHoliday(date),
                    IsCurrentMonth = false,
                });
            }
        }
        #endregion
    }
}
