using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ListOfExpensesAndIncomes
{
    internal static class Operations
    {
        public static double CalculatingBalance(BindingList<TransactionsModel> transactions)
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
                        //if (transactions[i].BalanceBeforeTransaction >= transactions[i].Summ)
                            transactions[i].BalanceAfterTransaction = transactions[i].BalanceBeforeTransaction - transactions[i].Summ;
                        //else break;
                    break;                    
                }
                //if (transactions[i].Type == "Income")
                //{
                //    transactions[i].BalanceAfterTransaction = transactions[i].BalanceBeforeTransaction + transactions[i].Summ;
                //}
                //else if (transactions[i].Type == "Purchase" && transactions[i].BalanceBeforeTransaction >= transactions[i].Summ)
                //{
                //    transactions[i].BalanceAfterTransaction = transactions[i].BalanceBeforeTransaction - transactions[i].Summ;
                //}
                i--;
            }
            while (i >= 0);
            return transactions[0].BalanceAfterTransaction;
        }
    }
}
