using ListOfExpensesAndIncomes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfExpensesAndIncomes.Services
{
    internal interface IWindowService
    {
        void ShowMainWindow(User user, ApplicationContext db);
    }
}
