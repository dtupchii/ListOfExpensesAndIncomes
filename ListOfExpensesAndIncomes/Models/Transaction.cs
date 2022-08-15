using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace ListOfExpensesAndIncomes.Models
{
    public class Transaction : INotifyPropertyChanged, ICloneable
    {
        public int Id { get; set; }
        private DateTime timeOfTransaction;
        private double summ;
        private string type;
        private string description;
        private double balanceAfterTransaction;
        public Transaction()
        {
            summ = 0;
            type = "";
            description = "";
        }
        public Transaction(DateTime dateTime, double summ, string type, string descr, User user)
        {
            timeOfTransaction = dateTime;
            this.summ = summ;
            this.type = type;
            description = descr;
            User = user;
            UserId = user.UserId;
        }
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

        [ForeignKey(nameof(User))]
        public int UserId { get; set; } //внешний ключ
        public User? User { get; set; } //навигационное свойство

        [NotMapped]
        public double BalanceBeforeTransaction { get; set; }

        [NotMapped]
        public double BalanceAfterTransaction
        {
            get => balanceAfterTransaction;
            set => Set(ref balanceAfterTransaction, value);
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propName = "")
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propName);
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public object Clone() => MemberwiseClone();
    }
}
