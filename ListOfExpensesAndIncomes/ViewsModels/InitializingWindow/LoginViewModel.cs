using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfExpensesAndIncomes.Services;
using ListOfExpensesAndIncomes.Core;
using ListOfExpensesAndIncomes.Models;
using System.Windows;
using ListOfExpensesAndIncomes.Views;
using System.Security;

namespace ListOfExpensesAndIncomes.ViewsModels.InitializingWindow
{
    public class LoginViewModel : ObservableObject
    {
        #region Fields
        ApplicationContext db;
        private User _userLogin = new User();
        private RelayCommand _loginCommand;
        InitializingViewVisibility isViewVisible;
        WindowNavigationService windowNav = new WindowNavigationService();
        #endregion
        public LoginViewModel(ApplicationContext db, InitializingViewVisibility IsViewVisible)
        {
            this.db = db;
            isViewVisible = IsViewVisible;
            UserLogin.Login = "";
            UserLogin.Password = "";
        }

        public User UserLogin
        {
            get { return _userLogin; }
            set
            {
                _userLogin = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand 
        {
            get
            {
                return _loginCommand ??
                    (_loginCommand = new RelayCommand(o => Enter(o, isViewVisible), o => (UserLogin.Login.Length > 0 && UserLogin.Password.Length > 0)));
            }
        }

        public void Enter(object o, InitializingViewVisibility IsViewVisible)
        {
            string log = UserLogin.Login.ToLower().Trim();
            if (db.Users.Where(b => b.Login == log).FirstOrDefault() == null || db.Users.Where(b => b.Password == UserLogin.Password).FirstOrDefault() == null)
            {
                MessageBox.Show("Login or password is wrong");
            }
            else if (db.Users.Where(b => b.Login == log && b.Password == UserLogin.Password).FirstOrDefault() != null)
            {
                User userFromDB = db.Users.Where(b => b.Login == log && b.Password == UserLogin.Password).FirstOrDefault();

                if (userFromDB != null)
                {
                    IWindowService windowSer = windowNav;
                    IsViewVisible.Visible = false;
                    windowSer.ShowMainWindow(userFromDB, db);
                }
                else MessageBox.Show("Unknown error occured");
            }            
        }
    }
}
