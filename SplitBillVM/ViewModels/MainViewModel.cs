using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SplitBillVM.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private int _guests;
        private int _tip;
        private int _bill;

        public MainViewModel()
        {
            Guests = 1;
            Tip = 5;
            Bill = 100;
            IncreaseGuestCommand = new Command(
                () =>
                {
                    Guests++;
                }
            );
            DecreaseGuestCommand = new Command(
                () =>
                {
                    Guests--;
                },
                () => { return Guests > 1; }
             );
            SetTipValueCommand = new Command<string>(
                (tip) =>
                {
                    Console.WriteLine("SetTipValueCommand: " + tip);
                    Tip = Int32.Parse(tip);
                }
                                          );
        }

        public Command IncreaseGuestCommand { get; set; }
        public Command DecreaseGuestCommand { get; set; }
        public Command<string> SetTipValueCommand { get; set; }

        public int Guests
        {
            get
            {
                return _guests;
            }
            set
            {
                _guests = value;
                OnPropertyChanged();
                OnPropertyChanged("BillPerGuest");
                OnPropertyChanged("BillWithTipPerGuest");
                DecreaseGuestCommand?.ChangeCanExecute();
            }
        }
        public int Tip
        {
            get
            {
                return _tip;
            }
            set
            {
                _tip = value;
                OnPropertyChanged();
                OnPropertyChanged("BillWithTip");
                OnPropertyChanged("BillPerGuest");
                OnPropertyChanged("BillWithTipPerGuest");
            }
        }
        public int Bill
        {
            get
            {
                return _bill;
            }
            set
            {
                _bill = value;
                OnPropertyChanged();
                OnPropertyChanged("BillWithTip");
                OnPropertyChanged("BillPerGuest");
                OnPropertyChanged("BillWithTipPerGuest");
            }
        }

        public float BillWithTip
        {
            get
            {
                return _bill * (100 + Tip) / 100;
            }
        }
        public float BillPerGuest
        {
            get
            {
                return Bill / Guests;
            }
        }

        public float BillWithTipPerGuest
        {
            get
            {
                return BillWithTip / Guests;
            }
        }

        #region MVVM
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
