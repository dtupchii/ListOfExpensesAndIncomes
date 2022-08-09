using ListOfExpensesAndIncomes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ListOfExpensesAndIncomes
{
    public class Transaction : INotifyPropertyChanged, ICloneable
    {
        private DateTime timeOfTransaction;
        private double summ;
        private string type;
        private string description;
        private double balanceAfterTransaction;
        public Transaction() { }
        public Transaction(DateTime dateTime, double summ, string type, string descr, int userId)
        {
            timeOfTransaction = dateTime;
            this.summ = summ;
            this.type = type;
            description = descr;
            UserId = userId;
        }
        public int Id { get; set; }

        public DateTime TimeOfTransaction
        {
            get => timeOfTransaction;
            set => Set(ref timeOfTransaction, value);
        }
        public double Summ
        {
            get => summ;
            set => Set(ref summ, value);
        }
        public string Type
        {
            get => type;
            set => Set(ref type, value);
        }
        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }
        public int UserId { get; set; }
        public User User { get; set; }

        [NotMapped]
        public double BalanceBeforeTransaction { get; set; }

        [NotMapped]
        public double BalanceAfterTransaction
        {
            get => balanceAfterTransaction;
            set => Set(ref balanceAfterTransaction, value);
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propName = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public object Clone() => MemberwiseClone();
    }
}       

