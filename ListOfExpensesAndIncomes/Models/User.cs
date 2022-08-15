using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ListOfExpensesAndIncomes.Models
{
    public class User
    {
        public int UserId { get; set; }
        public User()
        {
            Email = "";
            Login = "";
            Password = "";
            BeginningBalance = 0;
        }
        public User(string email, string login, string password, double beginningBalance)
        {
            Email = email;
            Login = login;
            Password = password;
            BeginningBalance = beginningBalance;
        }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public double BeginningBalance { get; set; }
        public BindingList<Transaction> Transactions { get; set; } = new BindingList<Transaction>();
    }
}
