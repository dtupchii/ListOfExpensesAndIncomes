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
using ListOfExpensesAndIncomes.Data;
using ListOfExpensesAndIncomes.Models;

namespace ListOfExpensesAndIncomes.Views
{
    /// <summary>
    /// Interaction logic for RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        User user;
        readonly ApplicationContext db;
        public RegistrationForm()
        {
            InitializeComponent();

            db = new ApplicationContext();

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
            login_tb.BorderBrush = Brushes.Transparent;
            password_tb.BorderBrush = Brushes.Transparent;
            email_tb.BorderBrush = Brushes.Transparent;

            string email = email_tb.Text.ToLower();
            string login = login_tb.Text.ToLower();
            string password = password_tb.Password;
            double beginningBalance = Convert.ToDouble(beginningBalance_tb.Text);

            if ((email.Length < 5 || !email.Contains("@") || !email.Contains(".")) && (login.Length < 5) && (password.Length < 7) && (beginningBalance < 0))
            {
                email_tb.BorderBrush = Brushes.Red;
            email_tb.ToolTip = "Enter correct email!";
                login_tb.BorderBrush = Brushes.Red;
                password_tb.BorderBrush = Brushes.Red;
                beginningBalance_tb.BorderBrush = Brushes.Red;
            }
            else if ((email.Length < 5 || !email.Contains("@") || !email.Contains(".")) && (login.Length < 5) && (password.Length < 7))
            {
                email_tb.ToolTip = "Enter correct email!";
                email_tb.BorderBrush = Brushes.Red;
                login_tb.BorderBrush = Brushes.Red;
                password_tb.BorderBrush = Brushes.Red;
            }
            else if ((email.Length < 5 || !email.Contains("@") || !email.Contains(".")) && (login.Length < 5) && (beginningBalance < 0))
            {
                email_tb.ToolTip = "Enter correct email!";
                email_tb.BorderBrush = Brushes.Red;
                login_tb.BorderBrush = Brushes.Red;
                beginningBalance_tb.BorderBrush = Brushes.Red;
            }
            else if ((email.Length < 5 || !email.Contains("@") || !email.Contains(".")) && (password.Length < 7) && (beginningBalance < 0))
            {
                email_tb.ToolTip = "Enter correct email!";
                email_tb.BorderBrush = Brushes.Red;
                password_tb.BorderBrush = Brushes.Red;
                beginningBalance_tb.BorderBrush = Brushes.Red;
            }
            else if ((login.Length < 5) && (password.Length < 7) && (beginningBalance < 0))
            {
                login_tb.BorderBrush = Brushes.Red;
                password_tb.BorderBrush = Brushes.Red;
                beginningBalance_tb.BorderBrush = Brushes.Red;
            }
            else if ((email.Length < 5 || !email.Contains("@") || !email.Contains(".")) && (login.Length < 5))
            {
                email_tb.ToolTip = "Enter correct email!";
                email_tb.BorderBrush = Brushes.Red;
                login_tb.BorderBrush = Brushes.Red;
            }
            else if ((email.Length < 5 || !email.Contains("@") || !email.Contains(".")) && (password.Length < 7))
            {
                email_tb.ToolTip = "Enter correct email!";
                email_tb.BorderBrush = Brushes.Red;
                password_tb.BorderBrush = Brushes.Red;
            }
            else if ((email.Length < 5 || !email.Contains("@") || !email.Contains(".")) && (beginningBalance < 0))
            {
                email_tb.ToolTip = "Enter correct email!";
                email_tb.BorderBrush = Brushes.Red;
                beginningBalance_tb.BorderBrush = Brushes.Red;
            }
            else if ((login.Length < 5) && (password.Length < 7))
            {
                login_tb.BorderBrush = Brushes.Red;
                password_tb.BorderBrush = Brushes.Red;
            }
            else if ((login.Length < 5) && (beginningBalance < 0))
            {
                login_tb.BorderBrush = Brushes.Red;
                beginningBalance_tb.BorderBrush = Brushes.Red;
            }
            else if ((password.Length < 7) && (beginningBalance < 0))
            {
                password_tb.BorderBrush = Brushes.Red;
                beginningBalance_tb.BorderBrush = Brushes.Red;
            }
            else if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                email_tb.ToolTip = "Enter correct email!";
                email_tb.BorderBrush = Brushes.Red;
            }
            else if (login.Length < 5)
            {
                login_tb.BorderBrush = Brushes.Red;
            }
            else if (password.Length < 7)
            {
                password_tb.BorderBrush = Brushes.Red;
            }
            else if (beginningBalance < 0)
            {
                beginningBalance_tb.BorderBrush = Brushes.Red;
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
                    user = new User(email, login, password, beginningBalance);
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
        }
    }
}
