using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfExpensesAndIncomes.Services;
using ListOfExpensesAndIncomes.Core;
using ListOfExpensesAndIncomes.Models;
using System.Text.RegularExpressions;
using System.Windows;
using System.ComponentModel;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ListOfExpensesAndIncomes.ViewsModels.InitializingWindow
{
    internal class RegistrationViewModel : ObservableObject
    {
        ApplicationContext db = new ApplicationContext();
        private User _userReg;
        private Currency _selectedCurrency;
        private RelayCommand _registrationCommand;
        InitializingViewVisibility isViewVisible;
        double bB;
        public RegistrationViewModel(ApplicationContext db, InitializingViewVisibility IsViewVisible)
        {
            this.db = db;
            isViewVisible = IsViewVisible;
            _userReg = new User { Email = "", Login = "", Password = "", BeginningBalance = "" };
            if (db.Currencies.ToList().Count > 0)
            {
                CurrenciesList = db.Currencies.ToList();
            }            
            else
            {
                CurrenciesList = new List<Currency>
                {
                    new Currency { Name = "EUR", Country = "Europa", Symbol = "€" },
                    new Currency { Name = "USD", Country = "USA", Symbol = "$" },
                    new Currency { Name = "GBP", Country = "Great Bretain", Symbol = "£"},
                    new Currency { Name = "UAH", Country = "Ukraine", Symbol = "₴" },
                    new Currency { Name = "PLN", Country = "Poland", Symbol = "zł" }
                };

                foreach (Currency c in CurrenciesList)
                {
                    db.Currencies.Add(c);
                }
                db.SaveChanges();
            }
            _selectedCurrency = CurrenciesList[0];
        }
        #region Properties
        public LoginViewModel LoginVM { get; set; }
        public RelayCommand RegistrationCommand
        {
            get
            {
                return _registrationCommand ??
                    (_registrationCommand = new RelayCommand(o => Registration(o, isViewVisible), 
                    o => CheckConditions()));
            }
        }
        public User UserReg
        {
            get { return _userReg; }
            set
            {
                _userReg = value;
                OnPropertyChanged();
            }
        }
        public List<Currency> CurrenciesList { get; set; }
        public Currency SelectedCurrency
        {
            get =>  _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        public void Registration(object o, InitializingViewVisibility IsViewVisible)
        {
            if (db.Users.Where(b => b.Email == UserReg.Email).FirstOrDefault() != null && db.Users.Where(b => b.Login == UserReg.Login).FirstOrDefault() != null)
            {
                MessageBox.Show("This email and login are already taken");
            }
            else if (db.Users.Where(b => b.Email == UserReg.Email).FirstOrDefault() != null)
            {
                MessageBox.Show("This email is already taken");
            }
            else if (db.Users.Where(b => b.Login == UserReg.Login).FirstOrDefault() != null)
            {
                MessageBox.Show("This login is already taken");
            }
            else if (!Double.TryParse(UserReg.BeginningBalance, out bB) )
            {
                MessageBox.Show("Please, enter correct summ");
            }
            else if (db.Users.Where(b => b.Email == UserReg.Email).FirstOrDefault() == null && db.Users.Where(b => b.Login == UserReg.Login).FirstOrDefault() == null)
            {
                string log = UserReg.Login.ToLower().Trim();
                string email = UserReg.Email.ToLower().Trim();
                string begB = UserReg.BeginningBalance.Trim();
                User newUser = new User { Login = log, Password = UserReg.Password, Email = email, BeginningBalance = begB, Currency = SelectedCurrency, CurrencyId = SelectedCurrency.CurrencyId };
                db.Users.Add(newUser);
                db.SaveChanges();
                MessageBox.Show("You are registered now");

                UserReg.Email = "";
                UserReg.Login = "";
                UserReg.Password = "";
                UserReg.BeginningBalance = "";
                SelectedCurrency = CurrenciesList[0];
            }
        }

        private bool CheckConditions()
        {
            string emailPattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}[a-z]*)+$";
            if (UserReg.Email.Length > 0 && Regex.IsMatch(UserReg.Email, emailPattern, RegexOptions.IgnoreCase) && UserReg.Login.Length > 0 &&
                UserReg.Password.Length > 5 && UserReg.BeginningBalance.Length > 0 && !UserReg.Login.Trim().Contains(' ') && !UserReg.Password.Trim().Contains(' '))
                return true;
            else
                return false;
        }
        #endregion
    }
}
