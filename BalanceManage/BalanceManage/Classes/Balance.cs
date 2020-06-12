using BalanceManage_Start.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceManage_Start.Classes
{
    //-----------------------------------
    //1. Balance 클래스를 수정하여 RemainingBalance의 변경 사항이 Notify되도록 하시오.
    //-----------------------------------
    public class Balance : INotifyPropertyChanged
    {
        private int remainingBalance;
        public int RemainingBalance
        {
            get
            {
                return remainingBalance;
            }
            set
            {
                    remainingBalance = value;
                    NotifyPropertyChanged("RemainingBalance");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propBalance)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propBalance));
        }

        public ExpendCommand expendCommand { get; set; }

        public Balance()
        {
            remainingBalance = 300000;
            expendCommand = new ExpendCommand(this);
        }
    }
}
