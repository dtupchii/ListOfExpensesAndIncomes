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
using System.Collections.ObjectModel;

namespace ListOfExpensesAndIncomes.ViewsModels
{
    public class HistoryViewModel : ObservableObject
    {
        private User _userHistory;
        private ApplicationContext _db;
        private Transaction _selectedTransaction;
        private RelayCommand _deleteCommand, _updateInfoCommand;
        public ObservableCollection<string> ItemList { get; set; } = new ObservableCollection<string> { "Income", "Purchase" };

        public HistoryViewModel(User user, ApplicationContext db)
        {
            _db = db;
            _userHistory = user;
            _selectedTransaction = new Transaction();
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

        public RelayCommand UpdateInfoCommand
        {
            get
            {
                return _updateInfoCommand ??= new RelayCommand(o =>
                {
                    try
                    {
                        Transaction transaction = o as Transaction;
                        Transaction changedTransactionInDB = _db.Transactions.Where(t => t.Id == transaction.Id).FirstOrDefault();
                        if (changedTransactionInDB != null)
                        {
                            if (changedTransactionInDB.Summ.Contains('-') && changedTransactionInDB.Type == "Income")
                            {
                                changedTransactionInDB.Summ = changedTransactionInDB.Summ.Remove(0, 1);
                            }
                            else if (!changedTransactionInDB.Summ.Contains('-') && changedTransactionInDB.Type == "Purchase")
                            {
                                changedTransactionInDB.Summ = changedTransactionInDB.Summ.Insert(0, "-");
                            }
                            _db.SaveChanges();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Couldn't convert o to Transaction type or couldn't find object in DataBase or something else went wrong");
                    }
                }, o => SelectedTransaction.Id != 0);
            }
        }
    }
}
