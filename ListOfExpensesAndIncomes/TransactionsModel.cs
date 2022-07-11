using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfExpensesAndIncomes
{
    internal class TransactionsModel : INotifyPropertyChanged
    {
        private DateTime _timeOfTransaction;
        private double _summ;
        private string _type;
        private string _description;
        private double _balanceBeforeTransaction;
        private double _balanceAfterTransaction;

        public TransactionsModel(DateTime dateTime, double summ, string type, string descr)
        {
            _timeOfTransaction = dateTime;
            _summ = summ;
            _type = type;
            _description = descr;
        }
        public double BalanceBeforeTransaction
        {
            get { return _balanceBeforeTransaction; }
            set {
                if (_balanceBeforeTransaction == value)
                    return;
                else
                {
                    _balanceBeforeTransaction = value;
                    OnPropertyChanged("BalanceAfterTransaction");
                }
            }
        }
        public double BalanceAfterTransaction
        {
            get { return _balanceAfterTransaction; }
            set {
                if (_balanceAfterTransaction == value)
                    return;
                else
                {
                    _balanceAfterTransaction = value;
                    OnPropertyChanged("BalanceAfterTransaction");
                }
            }
        }
        public DateTime TimeOfTransaction
        {
            get { return _timeOfTransaction; }
            set 
            { 
                if (_timeOfTransaction == value)
                    return;
                else
                {
                    _timeOfTransaction = value;
                    OnPropertyChanged("TimeOfTransaction");
                }
            }
        }
        public double Summ
        {
            get { return _summ; }
            set 
            { 
                if (_summ == value)
                    return;
                else
                {
                    Math.Round(value, 2);
                    _summ = value;
                    OnPropertyChanged("Summ");
                }
            }
        }
        public string Type
        {
            get { return _type; }
            set 
            {
                if (_type == value)
                    return;
                else
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;
                else
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
