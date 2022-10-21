using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using ListOfExpensesAndIncomes.Core;

namespace ListOfExpensesAndIncomes.Models
{
    public class Transaction : ObservableObject, ICloneable, IDataErrorInfo
    {
        public int Id { get; set; }
        private DateTime _timeOfTransaction = DateTime.Now.Date;
        private double _summ;
        private string _type;
        private string _description;
        private double _balanceAfterTransaction;
        public DateTime TimeOfTransaction
        {
            get => _timeOfTransaction;
            set => Set(ref _timeOfTransaction, value);
        }
        public double Summ
        {
            get => _summ;
            set => Set(ref _summ, value);
        }
        public string Type
        {
            get => _type;
            set => Set(ref _type, value);
        }
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; } //внешний ключ
        public User? User { get; set; } //навигационное свойство

        [NotMapped]
        public double BalanceBeforeTransaction { get; set; }

        [NotMapped]
        public double BalanceAfterTransaction
        {
            get => _balanceAfterTransaction;
            set => Set(ref _balanceAfterTransaction, value);
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propName = "")
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propName);
            return true;
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Summ":
                        if (Summ < 0)
                        {
                            error = "Amount should be over 0";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public object Clone() => new Transaction { TimeOfTransaction = TimeOfTransaction, Summ = Summ, Type = Type, Description = Description,
            User = new User { Email = User.Email, Login = User.Login, Password = User.Password, BeginningBalance = User.BeginningBalance } };
    }
}
