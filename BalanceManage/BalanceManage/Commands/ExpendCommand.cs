using BalanceManage_Start.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BalanceManage_Start.Commands
{
    public class ExpendCommand : ICommand
    {
        public Balance Balance { get; set; }

        public event EventHandler CanExecuteChanged;

        public ExpendCommand(Balance bal)
        {
            Balance = bal;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string expendStr = (string)parameter;
            int expendAmount = Int32.Parse(expendStr);
            Balance.RemainingBalance -= expendAmount;
        }
    }
}
