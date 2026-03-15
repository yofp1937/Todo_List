/*
 * Window창을 갖는 ViewModel들의 Base
 */
using Calendar.Common.Commands;
using Calendar.Common.Interface;
using Calendar.Common.Service;
using Calendar.Common.Util;
using System.Windows;
using System.Windows.Input;

namespace Calendar.ViewModel.Base
{
    public abstract class WindowBaseViewModel : BaseViewModel
    {
        private ITodoRepository? _todoRepository;
        protected ITodoRepository TodoRepository
        {
            get
            {
                if (_todoRepository == null)
                {
                    throw new InvalidOperationException($"[{GetType().Name}]: TodoRepository가 초기화되지 않았습니다.");
                }
                return _todoRepository;
            }
        }
        private ISettingRepository? _settingRepository;
        protected ISettingRepository SettingRepository
        {
            get
            {
                if (_settingRepository == null)
                {
                    throw new InvalidOperationException($"[{GetType().Name}]: SettingRepository가 초기화되지 않았습니다.");
                }
                return _settingRepository;
            }
        }
        // 최상단 타이틀바에 표시될 창 이름
        public string? MainBarTitleText { get; protected set; }

        #region Property
        public ICommand? DragMoveWindowCommand { get; private set; }
        public ICommand? MinimizeWindowCommand { get; private set; }
        public ICommand? MaximizeWindowCommand { get; private set; }
        public ICommand? CloseWindowCommand { get; private set; }
        #endregion

        #region 생성자
        protected WindowBaseViewModel(ITodoRepository? todoRepository, ISettingRepository? settingRepository)
        {
            _todoRepository = todoRepository;
            _settingRepository = settingRepository;
            RegisterICommands();
        }
        #endregion

        protected override void RegisterICommands()
        {
            DragMoveWindowCommand = new RelayCommand(MoveWindowExecute);
            MinimizeWindowCommand = new RelayCommand(MinimizeWindowExecute);
            MaximizeWindowCommand = new RelayCommand(MaximizeWindowExecute);
            CloseWindowCommand = new RelayCommand(CloseWindowExecute);
        }

        #region ICommand 연동 메서드
        private void MoveWindowExecute(object? obj) => WindowService.Instance.DragMove(obj);
        protected virtual void MinimizeWindowExecute(object? obj)
        {
            WindowService.Instance.Minimize(this);
        }
        private void MaximizeWindowExecute(object? obj) => WindowService.Instance.Maximize(this);
        private void CloseWindowExecute(object? obj) => CloseWindow();
        #endregion

        #region 메서드
        protected Window? GetWindowByDataContext()
        {
            return WindowService.Instance.GetWindowByViewModel(this);
        }

        /// <summary>
        /// 이 메서드를 호출한 ViewModel의 Window창을 닫음
        /// </summary>
        protected void CloseWindow() => WindowService.Instance.Close(this);

        /// <summary>
        /// Messenger에 구독중인 메세지 제거 요청
        /// </summary>
        public virtual void UnregisterMessages()
        {
            Messenger.Unregister(this);
        }
        #endregion
    }
}
