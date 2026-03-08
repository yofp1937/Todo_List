/*
 * 주간 반복 ViewModel
 */
using Calendar.Common.Interface;
using Calendar.Model.DataClass;
using Calendar.ViewModel.Base;
using System.Collections.ObjectModel;

namespace Calendar.ViewModel.TodoWindow.Routine
{
    public class WeeklyRoutineViewModel : BaseViewModel, IRoutineViewModel
    {
        #region Property
        // CheckBox와 Binding
        public ObservableCollection<WeeklyDayOption> DayOptions { get; }
        #endregion

        #region 생성자
        public WeeklyRoutineViewModel()
        {
            DayOptions = new ObservableCollection<WeeklyDayOption>
            {
                new WeeklyDayOption { DayName = "일", DayValue = DayOfWeek.Sunday, TextColor = "Crimson" },
                new WeeklyDayOption { DayName = "월", DayValue = DayOfWeek.Monday },
                new WeeklyDayOption { DayName = "화", DayValue = DayOfWeek.Tuesday },
                new WeeklyDayOption { DayName = "수", DayValue = DayOfWeek.Wednesday },
                new WeeklyDayOption { DayName = "목", DayValue = DayOfWeek.Thursday },
                new WeeklyDayOption { DayName = "금", DayValue = DayOfWeek.Friday },
                new WeeklyDayOption { DayName = "토", DayValue = DayOfWeek.Saturday, TextColor = "Blue" }
            };
        }
        #endregion

        #region interface 구현
        public object GetRoutineData()
        {
            return DayOptions.Where(x => x.IsSelected)
                                    .Select(x => x.DayValue)
                                    .ToList();
        }

        public void SetRoutineData(object data)
        {
            if (data is List<DayOfWeek> selectedDays)
            {
                foreach (DayOfWeek day in selectedDays)
                {
                    // DatOptions에서 day와 일치하는 첫번째 값을 target에 지정(없으면 null)
                    WeeklyDayOption? target = DayOptions.FirstOrDefault(x => x.DayValue == day);
                    if (target != null) target.IsSelected = true;
                }
            }
        }

        public bool GetEnteredRequireData()
        {
            return DayOptions.Any(x => x.IsSelected);
        }
        #endregion
    }
}
