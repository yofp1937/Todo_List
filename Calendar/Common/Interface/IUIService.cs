/*
 * 윈도우 창 생성(View 생성)을 ViewModel에서 직접 하면 안돼서 추상화 시키는 Interface
 */
using Calendar.Model.Enum;
using Calendar.ViewModel.Base;
using System.Windows;

namespace Calendar.Common.Interface
{
    public interface IUIService
    {
        #region Window 창 설정
        /// <summary>
        /// ViewModel과 DataContext로 연결된 Window를 찾아 반환
        /// </summary>
        Window? GetWindowByViewModel(object viewModel);
        /// <summary>
        /// ViewModel과 DataContext로 연결된 Window의 창 크기 변경
        /// </summary>
        void Minimize(object viewModel, MinimizeMode mode = MinimizeMode.Taskbar);
        /// <summary>
        /// ViewModel과 DataContext로 연결된 Window의 창 크기 변경
        /// </summary>
        void Maximize(object viewModel);
        /// <summary>
        /// ViewModel과 DataContext로 연결된 Window의 창을 닫음
        /// </summary>
        void Close(object viewModel);
        /// <summary>
        /// UIElement를 드래그하여 연결된 윈도우를 이동시킴
        /// </summary>
        void DragMove(object? obj);
        /// <summary>
        /// 메인 윈도우를 화면에 표시하고 활성화
        /// </summary>
        void RestoreMainWindow();
        /// <summary>
        /// 프로그램 종료 명령
        /// </summary>
        void ShutDown();
        #endregion
        #region Window 창 띄우기
        /// <summary>
        /// viewModel에 맞는 Window창을 생성해서 띄워주는 메서드
        /// </summary>
        bool? ShowDialog(BaseViewModel viewModel);

        /// <summary>
        /// EditTodoWindow를 MainViewModel, ListWindowViewModel 두곳에서 동일한 코드로 호출하고있어서<br/>
        /// 코드 재사용을 위해 IUIService로 이동
        /// </summary>
        bool? ShowEditWindow(object? obj, ITodoRepository todoRepository);
        #endregion
    }
}
