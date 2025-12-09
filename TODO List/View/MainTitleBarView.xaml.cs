using System.Windows;
using System.Windows.Input;

namespace TODO_List.View
{
    public partial class MainTitleBarView
    {
        public MainTitleBarView()
        {
            InitializeComponent();
        }

        private Window GetMainWindow()
        {
            return Window.GetWindow(this);
        }

        #region Opacity
        private void Slider_Opacity(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Window window = GetMainWindow();
            if (window != null)
            {
                double percent = e.NewValue; // 0~100
                window.Opacity = percent / 100.0; // WPF Opacity는 0~1
            }
        }
        #endregion

        #region OnClick
        private void OnClick_Minimize(object sender, RoutedEventArgs e)
        {
            Window window = GetMainWindow();
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void OnClick_Maximize(object sender, RoutedEventArgs e)
        {
            Window window = GetMainWindow();
            if (window != null)
            {
                if (window.WindowState == WindowState.Maximized)
                    window.WindowState = WindowState.Normal;
                else
                    window.WindowState = WindowState.Maximized;
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Window window = GetMainWindow();
            if (window != null)
            {
                window.Close();
            }
        }
        #endregion

        #region MoveWindow
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window window = GetMainWindow();
            if (window != null && e.LeftButton == MouseButtonState.Pressed)
            {
                window.DragMove();
            }
        }
        #endregion
    }
}
