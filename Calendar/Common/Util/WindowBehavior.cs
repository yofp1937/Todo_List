/*
 * 타이틀바를 Drag할 때 윈도우 창 이동을 위한 util 파일
 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Calendar.Common.Util
{
    public static class WindowBehavior
    {
        // DragMove를 활성화/비활성화하고 Command를 바인딩할 첨부 속성 정의
        // DependencyProperty: WPF 객체의 속성 값을 정의하는 특별한 속성(데이터 바인딩, 애니메이션, 스타일 등에 사용)
        public static readonly DependencyProperty MouseDownToDragMoveCommandProperty =
            DependencyProperty.RegisterAttached( // RegisterAttached: 첨부 속성(일반 속성과 달리 WPF 요소(Border 등)에 첨부하여 사용할 수 있는 속성)
                "MouseDownToDragMoveCommand", // XAML에서 사용할 속성 이름(Binding으로 매칭)
                typeof(ICommand), // MouseDownToDragMoveCommand의 타입을 ICommand로 설정
                typeof(WindowBehavior), // 이 첨부 속성을 등록한 class
                new PropertyMetadata(null, OnMouseDownToDragMoveCommandChanged)); // 속성의 기본값 = null, 값이 변경될때마다 뒤 함수 호출함

        // XAML에서 읽고 쓸 수 있도록 반드시 필요한 Get, Set 메서드
        public static ICommand GetMouseDownToDragMoveCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseDownToDragMoveCommandProperty);
        }

        public static void SetMouseDownToDragMoveCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseDownToDragMoveCommandProperty, value);
        }

        /// <summary>
        /// MouseDownToDragMoveCommand 속성의 값이 변경될 때 호출되는 콜백 메서드
        /// </summary>
        /// <param name="d">첨부 속성이 설정된 요소(XAML에서 MouseDownToDragMoveCommand가 등록된 요소)</param>
        /// <param name="e"></param>
        private static void OnMouseDownToDragMoveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                // 기존 이벤트 핸들러 제거
                element.MouseDown -= Element_MouseDown;

                // 새로운 Command가 설정되었다면 이벤트 핸들러 등록
                if (e.NewValue is ICommand command && command != null)
                {
                    element.MouseDown += Element_MouseDown;
                }
            }
        }

        /// <summary>
        /// MouseDown 이벤트 핸들러(XAML에서 이벤트가 등록된 요소를 클릭할때 호출되는 함수)
        /// </summary>
        private static void Element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is UIElement element && e.LeftButton == MouseButtonState.Pressed)
            {
                // 실제 클릭된 객체 가져오기
                DependencyObject? originalElement = e.OriginalSource as DependencyObject;
                // originalElement의 상위 객체들을 순회하며 상호작용 가능한 컨트롤이 있는지 검색
                if (IsInteractiveChild(originalElement))
                {
                    // 상호작용 가능한 컨트롤(버튼, 슬라이더 등)이 클릭됐으면 함수 종료
                    return;
                }

                // element에 Binding된 ICommand 타입의 속성(메서드)를 command에 등록
                ICommand command = GetMouseDownToDragMoveCommand(element);
                // command 실행 가능 여부 확인
                if (command != null && command.CanExecute(element))
                {
                    // Command 실행
                    command.Execute(element);
                    // 창 이동이 시작되면 이벤트가 더 이상 하위로 내려가지 않도록 처리 완료 표시
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// 주어진 요소의 비주얼 트리 상위에서 상호작용 컨트롤(Button, Slider 등)을 찾습니다.
        /// </summary>
        private static bool IsInteractiveChild(DependencyObject? current)
        {
            // TitleBar Border 자체는 제외해야 하므로, Window.GetWindow(current)와 current가 같지 않을 때까지만 탐색
            while (current != null && current != Window.GetWindow(current))
            {
                // TextBlock이나 Path 같은 단순한 시각 요소가 아닌, 상호작용 요소인지 확인
                if (current is System.Windows.Controls.Button || current is Slider || current is System.Windows.Controls.Primitives.ScrollBar)
                {
                    return true;
                }

                // 상위 요소로 이동
                current = VisualTreeHelper.GetParent(current);
            }
            // 최종적으로 상호작용 요소가 발견되지 않고 최상위 요소까지 도달한 경우
            return false;
        }
    }
}