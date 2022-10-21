using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ListOfExpensesAndIncomes.Core;

namespace ListOfExpensesAndIncomes.Models
{
    public class User : ObservableObject, IDataErrorInfo
    {
        private int _id;
        private string _login, _password, _email;
        private double _beginningBalance;
        private BindingList<Transaction> _transactions = new BindingList<Transaction>();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "BeginningBalance":
                        if (BeginningBalance < 0)
                        {
                            error = "BeginningBalance должен быть больше 0";
                        }
                        break;
                    case "Email":
                        if(!Email.Contains('@') || !Email.Contains('.') || Email.Length == 0)
                        {
                            error = "Email должен быть больше 0";
                        }
                            break;
                    case "Login":
                        if ((Login is null) || (Login.Length < 4))
                        {
                            error = "Login должен быть больше 6";
                        }
                        break;
                }
                return error;
            }
        }
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
        public double BeginningBalance
        {
            get { return _beginningBalance; }
            set
            {
                _beginningBalance = value;
                OnPropertyChanged();
            }
        }
        public BindingList<Transaction> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
