/*
 * 메인 UI에 할일 생성 ,업데이트해주는 클래스
 */
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TODO_List.Common.Commands;
using TODO_List.Common.Interface;
using TODO_List.Manager;
using TODO_List.Model;
using TODO_List.Model.DataClass.AddTodo;
using TODO_List.Model.Enum;
using TODO_List.ViewModel.Routine;

namespace TODO_List.ViewModel
{
    public class AddTodoViewModel : WindowBaseViewModel
    {
        #region Property
        // --- AddTodo Window창에서 사용하는 변수들 ---
        public bool IsRoutine { get; }
        public string MainBarTitleText { get; }
        private BaseViewModel? _currentRoutineVM;
        public BaseViewModel? CurrentRoutineVM
        {
            get => _currentRoutineVM;
            set =>SetProperty(ref _currentRoutineVM, value);
        }

        #region 일정, 규칙 공통 사용 Property
        // -- 공통 Binding 데이터 -- (제목, 시작 날짜, 내용)
        // 제목 TextBox
        private string _titleTextBox = string.Empty;
        public string TitleTextBox
        {
            get => _titleTextBox;
            set
            {
                // 1. TitleTextBox의 값이 변경됐고,
                // 2. IsWarnigVisible이 true이고,
                // 3. 입력된 값이 공백이 아니라면
                // IsWarningVisible = false 실행한다.
                if (SetProperty(ref _titleTextBox, value) && IsWarningVisible && !string.IsNullOrWhiteSpace(value))
                    IsWarningVisible = false;
            }
        }
        // 시작 날짜 DatePicker
        private DateTime _startDate = DateTime.Today;
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        // 상세 내용 TextBox
        private string _contentTextBox;
        public string ContentTextBox
        {
            get => _contentTextBox;
            set => SetProperty(ref _contentTextBox, value);
        }
        #endregion

        #region 규칙 추가에서만 사용되는 Property
        // --- Routine Binding 데이터 ---
        // 일간 ~ 연간 어떤 타입의 규칙 추가인지
        private RoutineType _selectedRoutineType;
        public RoutineType SelectedRoutineType
        {
            get => _selectedRoutineType;
            set
            {
                if (SetProperty(ref _selectedRoutineType, value))
                {
                    UpdateComboBoxItem();
                    UpdateRoutineViewModel();
                }
            }
        }

        // 종료 날짜 존재하는지 true = 미존재, false = 존재
        private bool _isIndefinite = true;
        public bool IsIndefinite
        {
            get => _isIndefinite;
            set => SetProperty(ref _isIndefinite, value);
        }
        // 종료 날짜
        private DateTime _endDate = DateTime.Now.AddYears(1);
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        // 빈도 ComboBox
        private int _selectedComboBoxItem;
        public int SelectedComboBoxItem
        {
            get => _selectedComboBoxItem;
            set => SetProperty(ref _selectedComboBoxItem, value);
        }

        private bool _isWarningVisible = false;
        public bool IsWarningVisible
        {
            get => _isWarningVisible;
            set => SetProperty(ref _isWarningVisible, value);
        }
        #endregion

        // UI 체크박스 바인딩용 Property
        public ObservableCollection<RoutineType> RoutineTypes { get; } = new ObservableCollection<RoutineType>();
        public ObservableCollection<string> ComboBoxItems { get; } = new ObservableCollection<string>();

        // 버튼 연결 Command
        public ICommand? RegisterCommand { get; private set; }
        public ICommand? CancleCommand { get; private set; }
        #endregion

        #region 생성자
        public AddTodoViewModel(bool isRoutine)
        {
            IsRoutine = isRoutine;
            MainBarTitleText = IsRoutine ? "규칙 추가" : "일정 추가";

            RegisterICommands();
            InitSetting();
        }
        #endregion

        #region 상속 구현
        protected override void RegisterICommands()
        {
            base.RegisterICommands();
            RegisterCommand = new RelayCommand(RegisterExecute);
            CancleCommand = new RelayCommand(CancleExecute);
        }
        #endregion

        #region 메서드
        /// <summary>
        /// TodoViewModel 생성시 필수 변수 설정
        /// </summary>
        private void InitSetting()
        {
            if (IsRoutine)
            {
                SelectedRoutineType = RoutineType.Daily;
            }
            else
            {
                SelectedRoutineType = RoutineType.None;
            }
        }

        /// <summary>
        /// 선택된 주기에따라 ViewModel 생성(View와 Context 매칭은 AddRoutineView.xaml의 ContentControl에서 진행)
        /// </summary>
        private void UpdateRoutineViewModel()
        {
            CurrentRoutineVM = SelectedRoutineType switch
            {
                RoutineType.Daily => new DailyRoutineViewModel(),
                RoutineType.Weekly => new WeeklyRoutineViewModel(),
                RoutineType.Monthly => new MonthlyRoutineViewModel(),
                RoutineType.Yearly => new YearlyRoutineViewModel(),
                _ => null
            };
            //Debug.WriteLine($"{CurrentRoutineVM}");
        }

        /// <summary>
        /// 주기에 맞게 ComboBox 업데이트 (추가하려면 반복문의 i 최대값만 바꾸면됨)
        /// </summary>
        private void UpdateComboBoxItem()
        {
            ComboBoxItems.Clear();

            // 현재 주기에 맞는 단위 설정
            string unit = SelectedRoutineType switch
            {
                RoutineType.Daily => "일",
                RoutineType.Weekly => "주",
                RoutineType.Monthly => "개월",
                RoutineType.Yearly => "년",
                _ => string.Empty
            };
            if (string.IsNullOrEmpty(unit)) return;

            // 반복문으로 리스트 생성
            for(int i = 1; i <= 10; i++)
            {
                if (i == 1)
                {
                    string specialName = SelectedRoutineType switch
                    {
                        RoutineType.Daily => "매일",
                        RoutineType.Weekly => "매주",
                        RoutineType.Monthly => "매월",
                        RoutineType.Yearly => "매년",
                        _ => ""
                    };
                    ComboBoxItems.Add(specialName);
                }
                else
                {
                    ComboBoxItems.Add($"{i}{unit}마다");
                }
            } 
            SelectedComboBoxItem = 0;
        }

        /// <summary>
        /// 등록 버튼 눌리면 (일정/규칙) 타입에 따라 데이터 추출해서 JSON 형식으로 저장
        /// </summary>
        private void RegisterExecute(object? obj)
        {
            // 1. 제목 입력 안돼있으면 return
            if (string.IsNullOrWhiteSpace(TitleTextBox))
            {
                IsWarningVisible = true;
                return;
            }

            if (IsRoutine)
            {
                RoutineData routineData = new RoutineData
                {
                    TodoText = TitleTextBox,
                    TodoDetail = ContentTextBox,
                    StartDate = StartDate,
                    RoutineType = SelectedRoutineType,
                    Frequency = SelectedComboBoxItem + 1,
                    IsIndefinite = IsIndefinite,
                    EndDate = IsIndefinite ? null : EndDate
                };

                if(CurrentRoutineVM is IRoutineViewModel routineVM)
                {
                    object tempData = routineVM.GetRoutineData();

                    // tempData의 데이터 형식에따라 주간, 월간, 연간 구분
                    switch (tempData)
                    {
                        case List<DayOfWeek> weekly: 
                            routineData.SelectedDays = weekly;
                            //Debug.WriteLine($"주간 규칙: {weekly.Count}");
                            break;
                        case List<int> monthly:
                            routineData.SelectedMonthDays = monthly;
                            //Debug.WriteLine($"월간 규칙: {monthly.Count}");
                            break;
                        case List<DateTime> yearly:
                            routineData.SelectedYearlyDates = yearly;
                            //Debug.WriteLine($"연간 규칙: {yearly.Count}");
                            break;
                        default:
                            break;
                    }
                }
                DataManager.Instance.AddRoutine(routineData);
            }
            else
            {
                ScheduleData scheduleData = new ScheduleData
                {
                    TodoText = TitleTextBox,
                    TodoDetail = ContentTextBox,
                    StartDate = StartDate
                };
                DataManager.Instance.AddSchedule(scheduleData);
            }
            CloseWindow();
        }

        private void CancleExecute(object? obj)
        {
            CloseWindow();
        }
        #endregion
    }
}
