using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WarehouseManager.UI.Common
{
    /// <summary>
    /// The RelayCommand class.
    /// Provides methods allowing to close a command into abstraction.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Defines if the action can be executed.
        /// </summary>
        /// <param name="parameters">Parameters passed into _canExecute predicate.</param>
        /// <returns>Returns if the action can be executed.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameters)
        {
            return _canExecute?.Invoke(parameters) ?? true;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="parameters">The parameters passed to Action during execution</param>
        public void Execute(object parameters)
        {
            _execute(parameters);
        }
    }
}
