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
        public static double CalculateBalance(BindingList<Transaction> transactions, double beginningBalance)
        {
            if (transactions.Count > 0)
            {
                int i = transactions.Count - 1;
                do
                {
                    if (i == transactions.Count - 1)
                        transactions[i].BalanceBeforeTransaction = beginningBalance;
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
            else return beginningBalance;
        }
    }
}
