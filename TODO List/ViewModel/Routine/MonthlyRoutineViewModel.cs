/*
 * 월간 반복 ViewModel
 */
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TODO_List.Common.Commands;
using TODO_List.Common.Interface;
using TODO_List.Model;

namespace TODO_List.ViewModel.Routine
{
    public class MonthlyRoutineViewModel : BaseViewModel, IRoutineViewModel
    {
        #region Property
        private int _selectedTempDay = 1;
        public int SelectedTempDay
        {
            get => _selectedTempDay;
            set => SetProperty(ref _selectedTempDay, value);
        }

        // ComboBox에 표시될 숫자 (1~31)
        public ObservableCollection<int> Days { get; } = new ObservableCollection<int>(Enumerable.Range(1, 31));

        // ListBox에 표시될 날짜들
        public ObservableCollection<int> SelectedMonthlyDates { get; } = new ObservableCollection<int>();

        // ICommand
        public ICommand? DateRegisterCommand { get; private set; }
        public ICommand? DateRemoveCommand { get; private set; }
        public ICommand? DatesClearCommand { get; private set; }
        #endregion

        #region 생성자
        public MonthlyRoutineViewModel()
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
        /// List<int> 형식으로 반환
        /// </summary>
        public object GetRoutineData()
        {
            return SelectedMonthlyDates.ToList();
        }
        #endregion

        #region ICommand 연동 메서드
        private void DateRegisterExecute(object? obj)
        {
            if (!SelectedMonthlyDates.Contains(SelectedTempDay))
            {
                SelectedMonthlyDates.Add(SelectedTempDay);

                // 오름차순 정렬
                var sorted = SelectedMonthlyDates.OrderBy(d => d).ToList();
                SelectedMonthlyDates.Clear();
                foreach (var d in sorted) SelectedMonthlyDates.Add(d);

                // ComboBox 초기화
                SelectedTempDay = 1;
            }
        }
        private void DateRemoveExecute(object? obj)
        {
            // SelectionMode="Extended"인 ListBox에서 CommandParameter로 값을 넘겨받으면 IList 형태로 넘어옴
            if (obj is IList selectedItems)
            {
                // 별도의 리스트에 복사 후 처리
                List<int> items = selectedItems.Cast<int>().ToList();

                foreach (int date in items)
                {
                    SelectedMonthlyDates.Remove(date);
                }
            }
        }
        private void DatesClearExecute(object? obj)
        {
            SelectedMonthlyDates.Clear();
        }
        #endregion
    }
}
