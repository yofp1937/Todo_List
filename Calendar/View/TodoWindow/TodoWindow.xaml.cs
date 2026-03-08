using Calendar.ViewModel.TodoWindow;
using System.Windows;

namespace Calendar.View.TodoWindow
{
    public partial class TodoWindow : Window
    {
        public TodoWindow(TodoBaseViewModel todoVM)
        {
            this.DataContext = todoVM;
            InitializeComponent();
        }
    }
}
