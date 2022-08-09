using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfExpensesAndIncomes.Models
{
    public class User
    {
        public int UserId { get; set; }
        private string login, password, email;
        private double beginningBalance;
        public User() { }
        public User(string email, string login, string password, double beginningBalance)
        {
            this.email = email;
            this.login = login;
            this.password = password;
            this.beginningBalance = beginningBalance;
        }
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public double BeginningBalance
        {
            get { return beginningBalance; }
            set { beginningBalance = value; }
        }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
