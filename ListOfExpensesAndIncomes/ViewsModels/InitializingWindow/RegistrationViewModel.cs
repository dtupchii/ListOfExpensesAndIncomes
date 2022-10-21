using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfExpensesAndIncomes.Services;
using ListOfExpensesAndIncomes.Core;
using ListOfExpensesAndIncomes.Models;
using System.Windows;

namespace ListOfExpensesAndIncomes.ViewsModels.InitializingWindow
{
    internal class RegistrationViewModel : ObservableObject
    {
        ApplicationContext db = new ApplicationContext();
        private User _user = new User { Email = "", Login = "", Password = ""};
        private RelayCommand _registrationCommand;
        InitializingViewVisibility isViewVisible;
        public RegistrationViewModel(ApplicationContext db, InitializingViewVisibility IsViewVisible)
        {
            this.db = db;
            isViewVisible = IsViewVisible;
            UserVM.Email = "user@g.co";
            UserVM.Login = "user";
        }

        #region Properties
        public LoginViewModel LoginVM { get; set; }
        public RelayCommand RegistrationCommand
        {
            get
            {
                return _registrationCommand ??
                    (_registrationCommand = new RelayCommand(o => Registration(o, isViewVisible), o => (UserVM.Email.Length > 0 && UserVM.Email.Contains('@') && UserVM.Email.Contains('.') && UserVM.Login.Length > 3 && UserVM.Password.Length > 5 && UserVM.BeginningBalance >= 0)));
            }
        }
        public User UserVM
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        public void Registration(object o, InitializingViewVisibility IsViewVisible)
        {
            if (db.Users.Where(b => b.Email == UserVM.Email).FirstOrDefault() != null && db.Users.Where(b => b.Login == UserVM.Login).FirstOrDefault() != null)
            {
                MessageBox.Show("This email and login are already taken");
            }
            else if (db.Users.Where(b => b.Email == UserVM.Email).FirstOrDefault() != null)
            {
                MessageBox.Show("This email is already taken");
            }
            else if (db.Users.Where(b => b.Login == UserVM.Login).FirstOrDefault() != null)
            {
                MessageBox.Show("This login is already taken");
            }
            else if (db.Users.Where(b => b.Email == UserVM.Email).FirstOrDefault() == null && db.Users.Where(b => b.Login == UserVM.Login).FirstOrDefault() == null)
            {
                string log = UserVM.Login.ToLower().Trim();
                string email = UserVM.Email.ToLower().Trim();
                User newUser = new User { Login = log, Password = UserVM.Password, Email = email, BeginningBalance = UserVM.BeginningBalance };
                db.Users.Add(newUser);
                db.SaveChanges();
                MessageBox.Show("You are registered now");
            }
        }
        #endregion
    }
}
