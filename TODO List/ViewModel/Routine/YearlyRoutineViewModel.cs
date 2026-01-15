/*
 * 연간 반복 ViewModel
 */
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TODO_List.Common.Commands;
using TODO_List.Common.Interface;
using TODO_List.Model;

namespace TODO_List.ViewModel.Routine
{
    public class YearlyRoutineViewModel : BaseViewModel, IRoutineViewModel
    {
        #region Property
        private DateTime? _selectedTempDate;
        public DateTime? SelectedTempDate
        {
            get => _selectedTempDate;
            set => SetProperty(ref _selectedTempDate, value);
        }

        // ListBox와 Binding
        public ObservableCollection<DateTime> SelectedYearlyDates { get; } = new ObservableCollection<DateTime>();

        // ICommand
        public ICommand? DateRegisterCommand { get; private set; }
        public ICommand? DateRemoveCommand { get; private set; }
        public ICommand? DatesClearCommand { get; private set; }
        #endregion

        #region 생성자
        public YearlyRoutineViewModel()
        {
            RegisterICommands();
        }
        #endregion

        #region 상속 구현
        protected override void RegisterICommands()
        {
            //Debug.WriteLine("YearlyRoutineViewModel - RegisterICommands");
            DateRegisterCommand = new RelayCommand(DateRegisterExecute);
            DateRemoveCommand = new RelayCommand(DateRemoveExecute);
            DatesClearCommand = new RelayCommand(DatesClearExecute);
        }

        /// <summary>
        /// List<DateTime> 형식으로 반환
        /// </summary>
        public object GetRoutineData()
        {
            return SelectedYearlyDates.ToList();
        }
        #endregion

        #region ICommand 연동 메서드
        private void DateRegisterExecute(object? obj)
        {
            if(SelectedTempDate.HasValue)
            {
                // 연도 정보는 2000년으로 고정
                DateTime dateToAdd = new DateTime(2000, SelectedTempDate.Value.Month, SelectedTempDate.Value.Day);

                if (!SelectedYearlyDates.Contains(dateToAdd))
                {
                    SelectedYearlyDates.Add(dateToAdd);

                    // 정렬 (월 -> 일 순으로 오름차순 정렬)
                    var sorted = SelectedYearlyDates.OrderBy(d => d.Month).ThenBy(d => d.Day).ToList();
                    SelectedYearlyDates.Clear();
                    foreach (var d in sorted) SelectedYearlyDates.Add(d);
                }

                // DatePicker 초기화
                SelectedTempDate = null;
            }
        }
        private void DateRemoveExecute(object? obj)
        {
            // SelectionMode="Extended"인 ListBox에서 CommandParameter로 값을 넘겨받으면 IList 형태로 넘어옴
            if(obj is IList selectedItems)
            {
                // 별도의 리스트에 복사 후 처리
                List<DateTime> items = selectedItems.Cast<DateTime>().ToList();

                foreach(DateTime date in items)
                {
                    SelectedYearlyDates.Remove(date);
                }
            }
        }
        private void DatesClearExecute(object? obj)
        {
            SelectedYearlyDates.Clear();
        }
        #endregion
    }
}
