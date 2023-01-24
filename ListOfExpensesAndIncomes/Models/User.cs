using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ListOfExpensesAndIncomes.Core;
using System.Text.RegularExpressions;

namespace ListOfExpensesAndIncomes.Models
{
    public class User : ObservableObject, IDataErrorInfo
    {
        private int _id;
        private string _login, _password, _email, _beginningBalance;
        private BindingList<Transaction> _transactions = new();

        public int UserId
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public string BeginningBalance
        {
            get { return _beginningBalance; }
            set
            {
                _beginningBalance = value;
                OnPropertyChanged();
            }
        }
        public Currency Currency { get; set; }  //навигационное свойство
        public int CurrencyId { get; set; } //внешний ключ
        public BindingList<Transaction> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }

        //Validation rules
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Email":
                        string pattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}[a-z]*)+$";
                        if ((Email.Length > 0) && (!Regex.IsMatch(Email, pattern, RegexOptions.IgnoreCase)))
                        {
                            error = "Email должен быть больше 0";
                        }
                        break;
                    case "BeginningBalance":
                        string bBPattern = @"(^[1-9]+(\d*)?(\.\d{1,2})?$)|(^[0-9]?\.\d{1,2}$)|(^0$)";
                        if (BeginningBalance.Length > 0 && !Regex.IsMatch(BeginningBalance, bBPattern))
                        {
                            error = "BeginningBalance должен быть больше 0";
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
    }
}
