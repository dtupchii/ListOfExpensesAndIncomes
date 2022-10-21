using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfExpensesAndIncomes.Services;
using ListOfExpensesAndIncomes.Core;
using ListOfExpensesAndIncomes.Models;
using ListOfExpensesAndIncomes.Services;
using System.Windows;
using System.ComponentModel;

namespace ListOfExpensesAndIncomes.ViewsModels
{
    public class HistoryViewModel : ObservableObject
    {
        private User _userHistory;
        private ApplicationContext _db;
        private Transaction _selectedTransaction;
        private RelayCommand _deleteCommand;

        public HistoryViewModel(User user, ApplicationContext db)
        {
            _db = db;
            _userHistory = user;
        }

        public User UserHistory
        {
            get { return _userHistory; }
            set
            {
                _userHistory = value;
                OnPropertyChanged();
            }
        }
        public Transaction SelectedTransaction
        {
            get { return _selectedTransaction; }
            set
            {
                _selectedTransaction = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand(o =>
                    {
                        Transaction transaction = o as Transaction;
                        if (transaction != null)
                        {
                            UserHistory.Transactions.Remove(transaction);
                            _db.Transactions.Remove(transaction);
                            _db.SaveChanges();
                            CopyOfList.MakeCopy(UserHistory.Transactions);
                        }
                else
                    MessageBox.Show("transaction is null");
                    }, o => UserHistory.Transactions.Count > 0));
            }
        }
    }
}
