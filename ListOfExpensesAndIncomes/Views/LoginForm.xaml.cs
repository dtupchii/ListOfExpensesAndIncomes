using ListOfExpensesAndIncomes.Data;
using ListOfExpensesAndIncomes.Models;
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

namespace ListOfExpensesAndIncomes.Views
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void GoToRegistrationForm(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.Show();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = login_tb.Text.ToLower(),
                   pass = password_tb.Password;
                        
            try
            {
                User loginUser = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    loginUser = db.Users.Where(b => b.Login == login && b.Password == pass).FirstOrDefault();
                }
                if (loginUser != null)
                {
                    MessageBox.Show("You entered correct data");
                    this.Hide();
                    MainWindow mW = new MainWindow(loginUser);
                    mW.Show();
                }
                else
                    MessageBox.Show("User doesn't exist");
            }
            catch
            {
                MessageBox.Show("Could'n find this user");
            }
        }
    }
}
