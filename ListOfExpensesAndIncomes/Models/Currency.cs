using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfExpensesAndIncomes.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Symbol { get; set; }
        public ObservableCollection<User> Users { get; set; }
    }
}
