using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ListOfExpensesAndIncomes.Models;

namespace ListOfExpensesAndIncomes.Services
{
    internal class CopyOfList
    {
        public static BindingList<Transaction> transactionsBC;
        public static void MakeCopy (BindingList<Transaction> transactions)
        {
            transactionsBC = new BindingList<Transaction>();
            foreach (Transaction t in transactions)
            {
                if (t != null)
                {
                    Transaction clone = (Transaction)t.Clone();
                    clone.Id = t.Id;
                    clone.BalanceAfterTransaction = t.BalanceAfterTransaction;
                    clone.BalanceBeforeTransaction = t.BalanceBeforeTransaction;
                    if (clone != null)
                        transactionsBC.Add(clone);
                }
            }
        }
    }
}
