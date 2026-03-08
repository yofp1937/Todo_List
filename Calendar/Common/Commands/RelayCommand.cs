/*
 * ViewModel에서 OnClick같은 데이터 처리 이벤트를 담당하게 만들기 위한 Util Class 
 */

/*
 * ViewModel에서 OnClick같은 데이터 처리 이벤트를 담당하게 만들기 위한 Util Class 
 */
using System.Windows.Input;

namespace Calendar.Common.Commands
{
    internal class RelayCommand : ICommand
    {
        #region Property
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region 생성자
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region 메서드
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute(parameter);
        #endregion
    }
}
