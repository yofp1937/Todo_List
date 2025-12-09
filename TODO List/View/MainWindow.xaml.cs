/*
 * 사용자가 보는 화면(View)을 담당
 */
using System.Windows;
using TODO_List.ViewModel;

namespace TODO_List.View
{
    public partial class MainWindow : Window
    {
        private MainViewModel _mvm;

        public MainWindow()
        {
            InitializeComponent();

            // DataContext 설정
            _mvm = new MainViewModel();
            DataContext = _mvm;
        }
    }
}