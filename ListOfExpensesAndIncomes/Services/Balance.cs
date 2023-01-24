using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ListOfExpensesAndIncomes.Models;

namespace ListOfExpensesAndIncomes.Services
{
    public class Balance
    {
        public static double CalculateBalance(BindingList<Transaction> transactions, string bB)
        {
            double beginningBalance = Double.Parse(bB);
            if (transactions.Count > 0)
            {
                int i = transactions.Count - 1;
                do
                {
                    if (i == transactions.Count - 1)
                        transactions[i].BalanceBeforeTransaction = beginningBalance;
                    else
                        transactions[i].BalanceBeforeTransaction = Math.Round(transactions[i + 1].BalanceAfterTransaction, 2);

                    transactions[i].BalanceAfterTransaction = Math.Round(transactions[i].BalanceBeforeTransaction + Double.Parse(transactions[i].Summ), 2);
                    
                    i--;
                }
                while (i >= 0);
                return transactions[0].BalanceAfterTransaction;
            }
            else return beginningBalance;
        }
    }
}
