using System.Windows;
using TODO_List.ViewModel;

namespace TODO_List.View.AddTodo
{
    public partial class AddTodoWindow : Window

    {
        public AddTodoWindow(AddTodoViewModel todoVM)
        {
            this.DataContext = todoVM;
            InitializeComponent();
        }
    }
}
