/*
 * 일정 목록을 눌렀을때 Window 호출
 */
using Calendar.ViewModel.ListWindow;
using System.Windows;

namespace Calendar.View.ListWindow
{
    public partial class ListWindow : Window
    {
        public ListWindow(ListWindowViewModel listVM)
        {
            this.DataContext = listVM;
            InitializeComponent();
        }
    }
}