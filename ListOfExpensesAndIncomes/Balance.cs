using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ListOfExpensesAndIncomes
{
    internal class Balance
    {
        //double currBalance;
        public double CurrentBalance { get; set; }
        public double CalculatingBalance(BindingList<TransactionsModel> transactions)
        {
            int i = transactions.Count - 1;
            do
            {
                if (i == transactions.Count - 1)
                    transactions[i].BalanceBeforeTransaction = 0;
                else
                    transactions[i].BalanceBeforeTransaction = transactions[i + 1].BalanceAfterTransaction;

                switch (transactions[i].Type)
                {
                    case "Income":
                        transactions[i].BalanceAfterTransaction = transactions[i].BalanceBeforeTransaction + transactions[i].Summ;
                        break;
                    case "Purchase":
                        transactions[i].BalanceAfterTransaction = transactions[i].BalanceBeforeTransaction - transactions[i].Summ;
                    break;                    
                }
                i--;
            }
            while (i >= 0);
            return transactions[0].BalanceAfterTransaction;
        }
    }
}
