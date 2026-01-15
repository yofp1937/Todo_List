/*
 * 화면에 표시될 Data를 담당
 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TODO_List.Common.Commands;
using TODO_List.Model;
using TODO_List.View.AddTodo;

namespace TODO_List.ViewModel
{
    public class MainViewModel : WindowBaseViewModel
    {
        #region Property
        public CalendarViewModel CalendarVM { get; private set; }
        public AddTodoViewModel? TodoVM { get; private set; }

        private double _windowOpacity = 100.0;
        public double WindowOpacity
        {
            get => _windowOpacity;
            set
            {
                if (SetProperty(ref _windowOpacity, value))
                {
                    ChangeWindowOpacityCommand?.Execute(value);
                }
            }
        }

        // MainTitleBar에 존재하는 버튼들은 MainViewModel에서 관리
        public ICommand? MinimizeWindowCommand { get; private set; }
        public ICommand? MaximizeWindowCommand { get; private set; }
        public ICommand? ChangeWindowOpacityCommand { get; private set; }
        public ICommand? PreviousMonthCommand { get; private set; }
        public ICommand? NextMonthCommand { get; private set; }
        public ICommand? CalendarChangeCommand { get; private set; }
        public ICommand? OpenAddTasksMenuCommand { get; private set; }
        public ICommand? AddScheduleCommand { get; private set; }
        public ICommand? AddRoutineCommand { get; private set; }
        #endregion

        #region 생성자, override
        public MainViewModel()
        {
            // UI 동적 생성
            CalendarVM = new CalendarViewModel();
        }

        protected override void RegisterICommands()
        {
            base.RegisterICommands();
            MinimizeWindowCommand = new RelayCommand(MinimizeWindowExecute);
            MaximizeWindowCommand = new RelayCommand(MaximizeWindowExecute);
            ChangeWindowOpacityCommand = new RelayCommand(ChangeWindowOpacityExecute);

            PreviousMonthCommand = new RelayCommand(_ => CalendarVM.CurrentMonth = CalendarVM.CurrentMonth.AddMonths(-1));
            NextMonthCommand = new RelayCommand(_ => CalendarVM.CurrentMonth = CalendarVM.CurrentMonth.AddMonths(1));
            CalendarChangeCommand = new RelayCommand(_ => { /* 달력 클릭 처리 */ });

            OpenAddTasksMenuCommand = new RelayCommand(OpenAddTasksMenuExecute);
            AddScheduleCommand = new RelayCommand(OpenAddScheduleWindowExecute); // 일정 추가 처리
            AddRoutineCommand = new RelayCommand(OpenAddRoutineWindowExecute); // 규칙 추가 처리
        }
        #endregion

        private void MinimizeWindowExecute(object? obj)
        {
            if (obj is Window window)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void MaximizeWindowExecute(object? obj)
        {
            if (obj is Window window)
            {
                window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
        }

        private void ChangeWindowOpacityExecute(object? obj)
        {
            if (obj is double opacityValue && Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Opacity = opacityValue / 100.0;
            }
        }

        private void OpenAddTasksMenuExecute(object? obj)
        {
            // Debug.WriteLine("OpenAddTasksMenuWindowExecute");
            if (obj is Button btn)
            {
                ContextMenu contextMenu = btn.ContextMenu;
                if (contextMenu != null)
                {
                    // ContextMenu가 버튼 아래에 위치되게 설정
                    contextMenu.PlacementTarget = btn;
                    contextMenu.Placement = PlacementMode.Bottom;
                    btn.ContextMenu.IsOpen = true;
                }
            }
        }

        private void OpenAddScheduleWindowExecute(object? obj)
        {
            // Debug.WriteLine("OpenAddScheduleWindowExecute");
            var todoVM = new AddTodoViewModel(false);
            
            var addWindow = new AddTodoWindow(todoVM);
            addWindow.Owner = Application.Current.MainWindow;
            addWindow.ShowDialog();
        }

        private void OpenAddRoutineWindowExecute(object? obj)
        {
            // Debug.WriteLine("OpenAddRoutineWindowExecute");
            var todoVM = new AddTodoViewModel(true);

            var addWindow = new AddTodoWindow(todoVM);
            addWindow.Owner = Application.Current.MainWindow;
            addWindow.ShowDialog();
        }
    }
}
