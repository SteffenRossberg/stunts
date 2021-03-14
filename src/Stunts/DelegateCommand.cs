using System;
using System.Windows.Input;

namespace Stunts
{
    class DelegateCommand : ICommand
    {
        public DelegateCommand(Action exec, Func<bool> canExec = null)
            : this(parameter => exec(), parameter => canExec?.Invoke() ?? true)
        {
        }

        public DelegateCommand(Action<object> exec, Func<object, bool> canExec = null)
        {
            _exec = exec;
            _canExec = canExec ?? (parameter => true);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public bool CanExecute(object parameter) => _canExec(parameter);
        public void Execute(object parameter) => _exec(parameter);

        private readonly Action<object> _exec;
        private readonly Func<object, bool> _canExec;
    }
}