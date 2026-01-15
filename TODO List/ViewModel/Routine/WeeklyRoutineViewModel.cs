/*
 * 주간 반복 ViewModel
 */
using System.Collections.ObjectModel;
using TODO_List.Common.Interface;
using TODO_List.Model;

namespace TODO_List.ViewModel.Routine
{
    public class WeeklyRoutineViewModel : BaseViewModel, IRoutineViewModel
    {
        #region Property
        // CheckBox와 Binding
        public ObservableCollection<WeeklyDayOptionViewModel> DayOptions { get; }
        #endregion

        #region 생성자
        public WeeklyRoutineViewModel()
        {
            DayOptions = new ObservableCollection<WeeklyDayOptionViewModel>
            {
                new WeeklyDayOptionViewModel { DayName = "일", DayValue = DayOfWeek.Sunday, TextColor = "Crimson" },
                new WeeklyDayOptionViewModel { DayName = "월", DayValue = DayOfWeek.Monday },
                new WeeklyDayOptionViewModel { DayName = "화", DayValue = DayOfWeek.Tuesday },
                new WeeklyDayOptionViewModel { DayName = "수", DayValue = DayOfWeek.Wednesday },
                new WeeklyDayOptionViewModel { DayName = "목", DayValue = DayOfWeek.Thursday },
                new WeeklyDayOptionViewModel { DayName = "금", DayValue = DayOfWeek.Friday },
                new WeeklyDayOptionViewModel { DayName = "토", DayValue = DayOfWeek.Saturday, TextColor = "Blue" }
            };
        }
        #endregion

        #region 상속 구현
        /// <summary>
        /// List<DayOfWeek> 형식으로 반환
        /// </summary>
        public object GetRoutineData()
        {
            return DayOptions.Where(x => x.IsSelected)
                                    .Select(x => x.DayValue)
                                    .ToList();
        }
        #endregion
    }
}
