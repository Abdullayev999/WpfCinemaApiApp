using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTools
{
    public class CommandBase
    {
        protected Func<bool> canExecute;
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke() ?? true;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
