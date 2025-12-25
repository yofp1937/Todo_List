/*
 * 메인 UI에 할일 생성 ,업데이트해주는 클래스
 */
using System.Collections.ObjectModel;
using System.Windows.Input;
using TODO_List.Common.Commands;
using TODO_List.Model;
using TODO_List.Model.DataClass;
using TODO_List.Model.Enum;

namespace TODO_List.ViewModel
{
    public class TodoViewModel : BaseViewModel
    {
        #region Property
        // 1. 데이터를 추가할때 일정 추가인지 규칙 추가인지 확인하는 용도
        public bool IsRoutine { get; }

        public string TitleText { get; }

        // 1-1. 일정 추가일 경우 일정의 날짜 저장
        private DateTime _dueDate = DateTime.Today;
        public DateTime DueDate
        {
            get => _dueDate;
            set 
            {
                _dueDate = value;
                OnPropertyChanged();
            }
        }

        // 1-2. 규칙 추가일 경우 규칙의 타입 저장
        private RootineType _selectedRootineType = RootineType.Daily;
        public RootineType SelectedRootineType
        {
            get => _selectedRootineType;
            set 
            {
                _selectedRootineType = value;
                OnPropertyChanged();
            }
        }

        // 일정 or 규칙의 내용 저장
        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        // UI 체크박스 바인딩용 Property
        public ObservableCollection<RootineType> RootineTypes { get; } = new ObservableCollection<RootineType>();

        // 버튼 연결 Command
        public ICommand? SaveCommand { get; private set; }
        public ICommand? CancelCommand { get; private set; }
        #endregion

        #region 생성자, override
        public TodoViewModel(bool isRoutine)
        {
            IsRoutine = isRoutine;

            TitleText = isRoutine ? "규칙 추가" : "일정 추가";
        }

        protected override void RegisterICommands()
        {
            SaveCommand = new RelayCommand(_ => { });
            CancelCommand = new RelayCommand(_ => { });
        }
        #endregion

        #region 메서드
        private bool CanSaveExecute(object obj)
        {
            // 내용이 비어있지 않은 경우에만 저장 가능
            return !string.IsNullOrWhiteSpace(Content);
        }

        private void SaveExecute(object obj)
        {
            // TODO: 할 일 객체 생성 및 메인 뷰모델에 전달/저장 로직 구현
            // obj는 주로 창 객체(Window)를 받아서 닫는 데 사용됩니다.

            if (IsRoutine)
            {
                // 반복 규칙 저장 로직 (TodoItem_Routine 생성)
                var newRoutine = new TodoItem_Routine
                {
                    Content = this.Content,
                    RootineType = this.SelectedRootineType,
                    // 다른 반복 속성들 추가
                };
                // MainViewModel의 Todo List에 추가 로직 필요
            }
            else
            {
                // 일반 일정 저장 로직 (TodoItem 생성)
                var newSchedule = new TodoItem
                {
                    Content = this.Content,
                    DueDate = this.DueDate,
                };
                // MainViewModel의 Todo List에 추가 로직 필요
            }

            // TODO: Add 창 닫기 로직 (View에서 처리되거나 obj를 통해 닫기 요청)
        }

        private void CancelExecute(object obj)
        {
            // TODO: Add 창 닫기 로직 구현
            // obj를 통해 창을 닫는 것이 일반적입니다.
        }
        #endregion
    }
}
