/*
 * Window창을 갖는 ViewModel들의 Base
 */
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TODO_List.Common.Commands;

namespace TODO_List.Model
{
    public abstract class WindowBaseViewModel : BaseViewModel
    {
        #region Property
        public ICommand? DragMoveWindowCommand { get; private set; }
        public ICommand? CloseWindowCommand { get; private set; }
        #endregion

        #region 생성자
        protected WindowBaseViewModel()
        {
            RegisterICommands();
        }
        #endregion

        protected override void RegisterICommands()
        {
            CloseWindowCommand = new RelayCommand(CloseWindowExecute);
            DragMoveWindowCommand = new RelayCommand(MoveWindowExecute);
        }

        #region ICommand 연동 메서드
        private void MoveWindowExecute(object? obj)
        {
            if (obj is UIElement element)
            {
                Window window = Window.GetWindow(element);
                if (window != null)
                {
                    // Debug.WriteLine("BaseViewModel - DragMoveWindowCommand - MoveWindowExecute");
                    window.DragMove();
                }
            }
        }

        private void CloseWindowExecute(object? obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }
        #endregion

        #region 메서드
        /// <summary>
        /// 이 메서드를 호출한 ViewModel의 Window창을 닫습니다
        /// </summary>
        protected void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
        #endregion
    }
}
