/*
 * Messenger에서 주고받는 Window 관련 메세지 정의 클래스
 */
using Calendar.ViewModel.Base;

namespace Calendar.Common.Messages
{
    public static class WindowMessages
    {
        public class UpdateOpacityMessage
        {
            public double Opacity { get; }
            public UpdateOpacityMessage(double opacity) => Opacity = opacity;
        }

        public class OpenWindowMessage
        {
            public BaseViewModel? ViewModel { get; }
            public object? Obj { get; }
            public OpenWindowMessage(BaseViewModel viewModel) => ViewModel = viewModel;
            public OpenWindowMessage(object obj) => Obj = obj;
        }
    }
}