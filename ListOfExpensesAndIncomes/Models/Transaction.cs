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
using System.Text.RegularExpressions;
using System.Threading;

namespace ListOfExpensesAndIncomes.Models
{
    public class Transaction : ObservableObject, ICloneable, IDataErrorInfo
    {
        public int Id { get; set; }
        private DateTime _timeOfTransaction = DateTime.Now.Date;
        private string _summ = String.Empty, _type, _description = String.Empty;
        private double _balanceAfterTransaction;
        private DateTime _date = DateTime.Now.Date;
        private string _hours = DateTime.Now.Hour.ToString();
        private string _minutes = DateTime.Now.Minute.ToString();
        object locker = new();
        public DateTime TimeOfTransaction
        {
            get => _timeOfTransaction;
            set => Set(ref _timeOfTransaction, value);
        }
        public string Summ
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
        [NotMapped]
        public DateTime Date
        {
            get => _date;
            set => Set(ref _date, value);
        }
        [NotMapped]
        public string Hours
        {
            get => _hours;
            set => Set(ref _hours, value);
            /*
            {
                int sum;
                if (Int32.TryParse(Hours, out sum))
                {
                    _hours = value;
                    OnPropertyChanged();
                }
            }*/
        }
        [NotMapped]
        public string Minutes
        {
            get => _minutes;
            set => Set(ref _minutes, value);
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propName = "")
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propName);
            return true;
        }

        //Validation
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Summ":
                        string summIncomePattern = @"(^[1-9]+(\d*)?(\.)?(\d{1,2})?$)|(^[0-9]?\.\d{1,2}$)|(^0$)",
                           summPurchasePattern = @"(^\-?[1-9]+(\d*)?(\.)?(\d{1,2})?$)|(^\-?[0-9]?\.\d{1,2}$)|(^0$)";
                        if ((Type == "Income" && Summ.Length > 0 && !Regex.IsMatch(Summ, summIncomePattern)) || (Type == "Purchase" && Summ.Length > 0 && !Regex.IsMatch(Summ, summPurchasePattern)))
                            error = "wrong summ";
                            break;

                    case "Hours":
                        if ((Hours.Length > 0) && (!Int32.TryParse(Hours, out _) || Int32.Parse(Hours) < 0 || Int32.Parse(Hours) > 23))
                        {
                            error = "Hours should be over 0";
                        }
                        break;

                    case "Minutes":
                        if (Minutes.Length > 0 && (!Int32.TryParse(Minutes, out _) || Int32.Parse(Minutes) < 0 || Int32.Parse(Minutes) > 59))
                        {
                            error = "Minutes should be over 0";
                        }
                        break;

                    //case "Date":
                    //    if (Date.ToString().Length > 0)
                    //    {
                    //        if (Date < 0)
                    //        {
                    //            error = "Amount should be over 0";
                    //        }
                    //    }
                    //    break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public object Clone() => new Transaction { TimeOfTransaction = TimeOfTransaction, Summ = Summ, Type = Type, Description = Description,
            User = new User { Email = User.Email, Login = User.Login, Password = User.Password, BeginningBalance = User.BeginningBalance, Currency = User.Currency, CurrencyId = User.CurrencyId } };


        //private void CheckingSumm(object? e)
        //{
        //    lock (locker)
        //    {
        //        string summIncomePattern = @"(^[1-9]+(\d*)?(\.)?(\d{1,2})?$)|(^[0-9]?\.\d{1,2}$)|(^0$)",
        //                   summPurchasePattern = @"(^\-?[1-9]+(\d*)?(\.)?(\d{1,2})?$)|(^\-?[0-9]?\.\d{1,2}$)|(^0$)";
        //        if (Type == "Income" && Summ.Length > 0 && !Regex.IsMatch(Summ, summIncomePattern))
        //        {
        //            e = "Amount should be over 0";
        //        }
        //        else if (Type == "Purchase" && Summ.Length > 0 && !Regex.IsMatch(Summ, summPurchasePattern))
        //        {
        //            e = "Enter correct amount";
        //        }
        //    }
        //}
    }
}
