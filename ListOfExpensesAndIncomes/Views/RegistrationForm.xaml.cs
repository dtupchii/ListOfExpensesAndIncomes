using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ListOfExpensesAndIncomes.Services;
using ListOfExpensesAndIncomes.Models;

namespace ListOfExpensesAndIncomes.Views
{
    /// <summary>
    /// Interaction logic for RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        readonly ApplicationContext db;
        public RegistrationForm(ApplicationContext db)
        {
            InitializeComponent();

            this.db = db;

            login_tb.ToolTip = "Login should be longer than 4 symbols";
            password_tb.ToolTip = "Password should be longer than 6 symbols";
            beginningBalance_tb.ToolTip = "Beginning balance should be over or equal 0";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            email_tb.BorderBrush = Brushes.Transparent;
            login_tb.BorderBrush = Brushes.Transparent;
            password_tb.BorderBrush = Brushes.Transparent;
            beginningBalance_tb.BorderBrush = Brushes.Transparent;

            string email = "", login = "", password = "";// coins = "";
            double beginningBalance = 0;

            if ((email_tb.Text.Length > 0) && (login_tb.Text.Length > 0) && (password_tb.Password.Length > 0) && (beginningBalance_tb.Text.Length > 0))
            {
                email = email_tb.Text.ToLower();
                login = login_tb.Text.ToLower();
                password = password_tb.Password.ToLower();
                beginningBalance = Convert.ToDouble(beginningBalance_tb.Text);
                string coins = beginningBalance.ToString();
                if ((coins.Contains('.')) || (coins.Contains(',')))
                {
                    if ((coins[coins.Length - 2] == '.') || (coins[coins.Length - 2] == ',') || (coins[coins.Length - 3] == '.') || (coins[coins.Length - 3] == ','))
                    {
                        #region Checking data
                        if (((email.Length < 5 && email.Length > 0) || !email.Contains("@") || !email.Contains(".")) && ((login.Length < 5) && (login.Length > 0)) && ((password.Length < 7) && (password.Length > 0)) && (beginningBalance < 0))
                        {
                            email_tb.BorderBrush = Brushes.Red;
                            email_tb.ToolTip = "Enter correct email!";
                            login_tb.BorderBrush = Brushes.Red;
                            login_tb.ToolTip = "Login should be longer than 4 symbols";
                            password_tb.BorderBrush = Brushes.Red;
                            password_tb.ToolTip = "Password should be longer than 6 symbols";
                            beginningBalance_tb.BorderBrush = Brushes.Red;
                            beginningBalance_tb.ToolTip = "Beginning balance should be positive";
                        }
                        else if (((email.Length < 5 && email.Length > 0) || !email.Contains("@") || !email.Contains(".")) && ((login.Length < 5) && (login.Length > 0)) && ((password.Length < 7) && (password.Length > 0)))
                        {
                            email_tb.BorderBrush = Brushes.Red;
                            email_tb.ToolTip = "Enter correct email!";
                            login_tb.BorderBrush = Brushes.Red;
                            login_tb.ToolTip = "Login should be longer than 4 symbols";
                            password_tb.BorderBrush = Brushes.Red;
                            password_tb.ToolTip = "Password should be longer than 6 symbols";
                        }
                        else if (((email.Length < 5 && email.Length > 0) || !email.Contains("@") || !email.Contains(".")) && ((login.Length < 5) && (login.Length > 0)) && (beginningBalance < 0))
                        {
                            email_tb.BorderBrush = Brushes.Red;
                            email_tb.ToolTip = "Enter correct email!";
                            login_tb.BorderBrush = Brushes.Red;
                            login_tb.ToolTip = "Login should be longer than 4 symbols";
                            beginningBalance_tb.BorderBrush = Brushes.Red;
                            beginningBalance_tb.ToolTip = "Beginning balance should be positive";
                        }
                        else if (((email.Length < 5 && email.Length > 0) || !email.Contains("@") || !email.Contains(".")) && ((password.Length < 7) && (password.Length > 0)) && (beginningBalance < 0))
                        {
                            email_tb.BorderBrush = Brushes.Red;
                            email_tb.ToolTip = "Enter correct email!";
                            password_tb.BorderBrush = Brushes.Red;
                            password_tb.ToolTip = "Password should be longer than 6 symbols";
                            beginningBalance_tb.BorderBrush = Brushes.Red;
                            beginningBalance_tb.ToolTip = "Beginning balance should be positive";
                        }
                        else if (((login.Length < 5) && (login.Length > 0)) && ((password.Length < 7) && (password.Length > 0)) && (beginningBalance < 0))
                        {
                            login_tb.BorderBrush = Brushes.Red;
                            login_tb.ToolTip = "Login should be longer than 4 symbols";
                            password_tb.BorderBrush = Brushes.Red;
                            password_tb.ToolTip = "Password should be longer than 6 symbols";
                            beginningBalance_tb.BorderBrush = Brushes.Red;
                            beginningBalance_tb.ToolTip = "Beginning balance should be positive";
                        }
                        else if (((email.Length < 5 && email.Length > 0) || !email.Contains("@") || !email.Contains(".")) && ((login.Length < 5) && (login.Length > 0)))
                        {
                            email_tb.BorderBrush = Brushes.Red;
                            email_tb.ToolTip = "Enter correct email!";
                            login_tb.BorderBrush = Brushes.Red;
                            login_tb.ToolTip = "Login should be longer than 4 symbols";
                        }
                        else if (((email.Length < 5 && email.Length > 0) || !email.Contains("@") || !email.Contains(".")) && ((password.Length < 7) && (password.Length > 0)))
                        {
                            email_tb.BorderBrush = Brushes.Red;
                            email_tb.ToolTip = "Enter correct email!";
                            password_tb.BorderBrush = Brushes.Red;
                            password_tb.ToolTip = "Password should be longer than 6 symbols";
                        }
                        else if (((email.Length < 5 && email.Length > 0) || !email.Contains("@") || !email.Contains(".")) && (beginningBalance < 0))
                        {
                            email_tb.BorderBrush = Brushes.Red;
                            email_tb.ToolTip = "Enter correct email!";
                            beginningBalance_tb.BorderBrush = Brushes.Red;
                            beginningBalance_tb.ToolTip = "Beginning balance should be positive";
                        }
                        else if (((login.Length < 5) && (login.Length > 0)) && ((password.Length < 7) && (password.Length > 0)))
                        {
                            login_tb.BorderBrush = Brushes.Red;
                            login_tb.ToolTip = "Login should be longer than 4 symbols";
                            password_tb.BorderBrush = Brushes.Red;
                            password_tb.ToolTip = "Password should be longer than 6 symbols";
                        }
                        else if (((login.Length < 5) && (login.Length > 0)) && (beginningBalance < 0))
                        {
                            login_tb.BorderBrush = Brushes.Red;
                            login_tb.ToolTip = "Login should be longer than 4 symbols";
                            beginningBalance_tb.BorderBrush = Brushes.Red;
                            beginningBalance_tb.ToolTip = "Beginning balance should be positive";
                        }
                        else if (((password.Length < 7) && (password.Length > 0)) && (beginningBalance < 0))
                        {
                            password_tb.BorderBrush = Brushes.Red;
                            password_tb.ToolTip = "Password should be longer than 6 symbols";
                            beginningBalance_tb.BorderBrush = Brushes.Red;
                            beginningBalance_tb.ToolTip = "Beginning balance should be positive";
                        }
                        else if ((email.Length < 5 && email.Length > 0) || !email.Contains("@") || !email.Contains("."))
                        {
                            email_tb.BorderBrush = Brushes.Red;
                            email_tb.ToolTip = "Enter correct email!";
                        }
                        else if ((login.Length < 5) && (login.Length > 0))
                        {
                            login_tb.BorderBrush = Brushes.Red;
                            login_tb.ToolTip = "Login should be longer than 4 symbols";
                        }
                        else if ((password.Length < 7) && (password.Length > 0))
                        {
                            password_tb.BorderBrush = Brushes.Red;
                            password_tb.ToolTip = "Password should be longer than 6 symbols";
                        }
                        else if (beginningBalance < 0)
                        {
                            beginningBalance_tb.BorderBrush = Brushes.Red;
                            beginningBalance_tb.ToolTip = "Beginning balance should be positive";
                        }
                        else
                        {
                            login_tb.ToolTip = "";
                            login_tb.BorderBrush = Brushes.Transparent;
                            password_tb.ToolTip = "";
                            password_tb.BorderBrush = Brushes.Transparent;
                            email_tb.ToolTip = "";
                            email_tb.BorderBrush = Brushes.Transparent;
                            beginningBalance_tb.ToolTip = "";
                            beginningBalance_tb.BorderBrush = Brushes.Transparent;

                            try
                            {
                                User user = new User(email, login, password, beginningBalance);
                                db.Users.Add(user);
                                db.SaveChanges();
                                MessageBox.Show("You are registered now!");

                                this.Hide();
                                LoginForm loginForm = new LoginForm();
                                loginForm.Show();
                            }
                            catch
                            {
                                MessageBox.Show("Data not saved :(");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        beginningBalance_tb.BorderBrush = Brushes.Red;
                        beginningBalance_tb.ToolTip = "Balance should have format <100.00>";
                        grid.ToolTip = "Enter correct data";
                    }
                }
                else
                {
                    beginningBalance_tb.BorderBrush = Brushes.Red;
                    beginningBalance_tb.ToolTip = "Enter correct balance!";
                }
            }
            else
            {
                if (!(email_tb.Text.Length > 0))
                {
                    email_tb.BorderBrush = Brushes.Red;
                    email_tb.ToolTip = "Enter email!";
                }
                if (!(login_tb.Text.Length > 0))
                {
                    login_tb.BorderBrush = Brushes.Red;
                    login_tb.ToolTip = "Create login!";
                }
                if (!(password_tb.Password.Length > 0))
                {
                    password_tb.BorderBrush = Brushes.Red;
                    password_tb.ToolTip = "Create password!";
                }
                if (!(beginningBalance_tb.Text.Length > 0))
                {
                    beginningBalance_tb.BorderBrush = Brushes.Red;
                    beginningBalance_tb.ToolTip = "Enter beginning balance!";
                }
            }
        }
    }
}
