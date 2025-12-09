/*
 * 화면에 표시될 Data를 담당
 */
using System.Windows.Input;
using TODO_List.Model;

namespace TODO_List.ViewModel
{
    internal class MainViewModel
    {
        #region Property
        public CalendarViewModel CalendarVM { get; private set; }

        public ICommand PreviousMonthCommand { get; private set; }
        public ICommand NextMonthCommand { get; private set; }
        public ICommand CalendarChangeCommand { get; private set; }
        #endregion

        #region 생성자
        public MainViewModel()
        {
            // UI 동적 생성
            CalendarVM = new CalendarViewModel();
            CreateRelayCommand();
        }
        #endregion

        #region RelayCommand 연결
        private void CreateRelayCommand()
        {
            PreviousMonthCommand = new RelayCommand(_ => CalendarVM.CurrentMonth = CalendarVM.CurrentMonth.AddMonths(-1));
            NextMonthCommand = new RelayCommand(_ => CalendarVM.CurrentMonth = CalendarVM.CurrentMonth.AddMonths(1));
            CalendarChangeCommand = new RelayCommand(_ => { /* 달력 클릭 처리 */ });
        }
        #endregion
    }
}
