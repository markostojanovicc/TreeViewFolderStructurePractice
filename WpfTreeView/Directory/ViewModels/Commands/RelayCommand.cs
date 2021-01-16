

using System;
using System.Windows.Input;

namespace WpfTreeView.Directory.ViewModels.Commands
{
    public class RelayCommand : ICommand
    {
        private Action mAction;

        public event EventHandler CanExecuteChanged = (sender, e) => {};

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        /// <summary>
        /// can always be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();  
        }
    }
}
