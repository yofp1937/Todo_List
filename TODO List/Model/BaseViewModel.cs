/*
 * INotifyPropertyChaged를 사용하는 View들의 기반 클래스
 */
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TODO_List.Common.Commands;

namespace TODO_List.Model
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Property
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand? DragMoveWindowCommand { get; private set; }
        public ICommand? CloseWindowCommand { get; private set; }
        #endregion

        #region 생성자
        public BaseViewModel()
        {
            CloseWindowCommand = new RelayCommand(CloseWindowExecute);
            DragMoveWindowCommand = new RelayCommand(MoveWindowExecute);

            RegisterICommands(); // 모든 자식 클래스에서 강제로 실행됨
        }
        #endregion

        #region INotifyPropertyChanged 메서드
        /// <summary>
        /// 값이 변할때 UI를 갱신시키기위한 알림 함수
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///  새로 입력받은 값이 기존 값과 다르다면 이벤트 호출 이후 True 반환
        /// </summary>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            // 기존 값과 새 값이 같다면 아무것도 하지 않고 false 반환
            if (Equals(storage, value))
            {
                return false;
            }

            // 값이 다르다면 storage에 새 값 저장
            storage = value;

            // UI 업데이트를 위해 PropertyChanged 이벤트 발생
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion

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

        #region abstract 메서드
        /// <summary>
        /// BaseViewModel을 상속받는 ViewModel들에서 ICommand를 초기화하는 로직을 구현하도록 강제한다<br/>
        /// RegisterICommands는 생성자에서 자동 실행되므로 따로 호출 할 필요 없음
        /// </summary>
        protected abstract void RegisterICommands();
        #endregion
    }
}
