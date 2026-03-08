/*
 * 캘린더 UI 동적 생성용 데이터 클래스
 */

using Calendar.Model.DataClass.TodoEntities;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Calendar.Model.DataClass
{
    public class CalendarDayModel : BaseDataClass
    {
        #region Property
        public DateTime Date { get; private set; }
        public int DayNumber => Date.Day;
        public DayOfWeek DayOfWeek => Date.DayOfWeek;
        public bool IsCurrentMonth { get; private set; }
        public bool IsToday { get; private set; }
        public bool IsHoliday { get; private set; }
        public string? HolidayName { get; private set; }

        // Calendar에서 선택된 날짜를 표시하기위해 선 굵기를 변경하는데 해당 기능을 위해 필요
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public ObservableCollection<ScheduleData> Schedules { get; set; } = new();
        public ObservableCollection<RoutineInstance> RoutineInstances { get; set; } = new();

        // SidePanel, Calendar에서 Schedules, Routines를 한번에 읽어가기위한 용도
        private ICollectionView _allTasksView = null!;
        public ICollectionView AllTasksView
        {
            get
            {
                if (_allTasksView == null)
                {
                    // 최초 1회 생성 및 정렬 규칙 설정
                    var combined = Schedules.Cast<object>().Concat(RoutineInstances.Cast<object>());
                    _allTasksView = CollectionViewSource.GetDefaultView(combined);

                    // 속성값이 변할 때 실시간으로 정렬에 반영되도록 설정
                    if (_allTasksView is ICollectionViewLiveShaping liveView && liveView.CanChangeLiveSorting)
                    {
                        liveView.IsLiveSorting = true;
                        liveView.LiveSortingProperties.Add(nameof(BaseTodoDataWithStatus.Status.Failure));
                        liveView.LiveSortingProperties.Add(nameof(BaseTodoDataWithStatus.Status.Completion));
                    }

                    // 사용자님의 정렬 기준 적용 (최초)
                    ApplySortDescriptions();
                }
                return _allTasksView;
            }
        }

        // AllTasksView의 갯수가 10개를 넘어가면 
        public bool IsTaskOverLimit => AllTasksView.Cast<object>().Count() >= 10;
        #endregion

        #region 생성자
        public CalendarDayModel()
        {
            // 컬렉션이 새로 할당되거나 변경될 때를 위해 이벤트 연결
            Schedules.CollectionChanged += (s, e) => { SubscribeItems(e.NewItems); OnPropertyChanged(nameof(AllTasksView)); };
            RoutineInstances.CollectionChanged += (s, e) => { SubscribeItems(e.NewItems); OnPropertyChanged(nameof(AllTasksView)); };
        }
        public CalendarDayModel(DateTime date, bool isCurrentMonth) : this()
        {
            Date = date.Date;
            IsToday = date == DateTime.Today;
            IsCurrentMonth = isCurrentMonth;

            HolidayInfo hoildayInfo = HolidayProvider.GetHolidayInfo(date);
            IsHoliday = hoildayInfo.IsHoliday;
            HolidayName = hoildayInfo.HolidayName;
        }
        #endregion

        #region 메서드
        /// <summary>
        /// Schedules, Routines 초기화
        /// </summary>
        public void DataClear()
        {
            Schedules.Clear();
            RoutineInstances.Clear();
        }

        /// <summary>
        /// AllTasksView에 정렬 적용
        /// </summary>
        private void ApplySortDescriptions()
        {
            _allTasksView.SortDescriptions.Clear();
            // 1. 상태(Status) 기준 정렬 
            _allTasksView.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Ascending));
            // 2. 타입(TypePriority) 기준 정렬
            _allTasksView.SortDescriptions.Add(new SortDescription("TypePriority", ListSortDirection.Ascending));
            // 3. 등록 순서(CreatedTicks) 기준 정렬
            _allTasksView.SortDescriptions.Add(new SortDescription("CreatedTicks", ListSortDirection.Ascending));
        }

        /// <summary>
        /// IsCompleted, IsFailure 등 아이템 내부의 속성이 변경될때(정렬을 다시 해야할때) UI 갱신을 위해 PropertyChanged와 연결
        /// </summary>
        /// <param name="items"></param>
        private void SubscribeItems(IList? items)
        {
            if (items == null) return;

            foreach (var item in items)
            {
                if (item is ScheduleData schedule)
                {
                    schedule.PropertyChanged += OnTaskPropertyChanged;
                }
                // 2. 루틴(RoutineInstance)인 경우: 내부의 Record가 Status를 가짐
                else if (item is RoutineInstance routineInstance)
                {
                    routineInstance.PropertyChanged += OnTaskPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Status가 변경되면 AllTasksView.Refresh를 호출하여 정렬을 다시 수행함
        /// </summary>
        private void OnTaskPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // Status가 변경될 때만 정렬 새로고침
            if (e.PropertyName == nameof(BaseTodoDataWithStatus.Status))
            {
                // UI 스레드에서 안전하게 Refresh 호출
                Application.Current.Dispatcher.Invoke(() =>
                {
                    RefreshView();
                });
            }
        }

        /// <summary>
        /// 입력된 일정, 규칙을 Refresh로 새로 정렬하고 OnPropertyChanged를 호출해 UI에 갱신시키게 만듦
        /// </summary>
        public void RefreshView()
        {
            AllTasksView.Refresh();
            // AllTasksView 속성이 업데이트됐음을 UI에게 알림
            OnPropertyChanged(nameof(AllTasksView));
            OnPropertyChanged(nameof(IsTaskOverLimit));
        }
        #endregion
    }
}