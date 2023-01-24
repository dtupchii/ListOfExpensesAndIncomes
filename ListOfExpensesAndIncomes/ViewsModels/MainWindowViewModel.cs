using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ListOfExpensesAndIncomes.Services;
using ListOfExpensesAndIncomes.Models;
using System.ComponentModel;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Threading;
using ListOfExpensesAndIncomes.Core;

namespace ListOfExpensesAndIncomes.ViewsModels
{
    public class MainWindowViewModel : ObservableObject
    {
        readonly ApplicationContext db;
        object locker = new();
        private object _currentView;
        private double _currentBalance;


        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public double CurrentBalance
        {
            get { return _currentBalance; }
            set
            {
                _currentBalance = value;
                OnPropertyChanged();
            }
        }
        public User UserMainV { get; set; }
        public Action CloseAction { get; set; }
        public Action MinimizeAction { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand MinimizeCommand { get; set; }
        public RelayCommand NewDataViewCommand { get; set; }
        public RelayCommand HistoryViewCommand { get; set; }
        public NewDataViewModel NewDataVM { get; set; }
        public HistoryViewModel HistoryVM { get; set; }


        public MainWindowViewModel(User user, ApplicationContext db)
        {
            this.db = db;
            UserMainV = user;

            try
            {
                db.Transactions.Where(b => b.UserId == user.UserId).OrderByDescending(c => c.TimeOfTransaction).Load();
                user.Transactions = db.Transactions.Local.ToBindingList();
                user.Currency = db.Currencies.Where(c => c.CurrencyId == user.CurrencyId).FirstOrDefault();
            }
            catch
            {
                user.Transactions = new BindingList<Transaction>();
                MessageBox.Show("Couldn't load data when window loaded");
            }


            CurrentBalance = Balance.CalculateBalance(user.Transactions, user.BeginningBalance);
            user.Transactions.ListChanged += TransactionList_ListChanged;

            CopyOfList.MakeCopy(user.Transactions);

            NewDataVM = new NewDataViewModel(user, db);
            HistoryVM = new HistoryViewModel(user, db);

            CurrentView = NewDataVM;

            NewDataViewCommand = new RelayCommand(o =>
            {
                CurrentView = NewDataVM;
            });
            HistoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = new HistoryViewModel(user, db);
            });

            CloseCommand = new RelayCommand(o => CloseAction());
            MinimizeCommand = new RelayCommand(o => MinimizeAction());
        }


        private void TransactionList_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (CopyOfList.transactionsBC.Count == UserMainV.Transactions.Count)
                {
                    for (int i = 0; i < UserMainV.Transactions.Count; i++)
                    {
                        if ((CopyOfList.transactionsBC[i].TimeOfTransaction != UserMainV.Transactions[i].TimeOfTransaction) || (CopyOfList.transactionsBC[i].Summ != UserMainV.Transactions[i].Summ) ||
                        (CopyOfList.transactionsBC[i].Type != UserMainV.Transactions[i].Type) || (CopyOfList.transactionsBC[i].Description != UserMainV.Transactions[i].Description))
                        {
                            Thread mainThread = Thread.CurrentThread;
                            Thread thread = new Thread(SaveChanges);
                            thread.Start(i);
                            thread.Join();
                            CurrentBalance = Balance.CalculateBalance(UserMainV.Transactions, UserMainV.BeginningBalance);
                            CopyOfList.MakeCopy(UserMainV.Transactions);

                            break;
                        }
                        else if (CopyOfList.transactionsBC[i].BalanceAfterTransaction != UserMainV.Transactions[i].BalanceAfterTransaction)
                            break;

                    }
                }
            }
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (UserMainV.Transactions.Count > 0)
                {
                    var sortedListInstance = new BindingList<Transaction>(UserMainV.Transactions.OrderByDescending(x => x.TimeOfTransaction).ToList());
                    UserMainV.Transactions = sortedListInstance;
                }
                CurrentBalance = Balance.CalculateBalance(UserMainV.Transactions, UserMainV.BeginningBalance);
            }
        }

        private void SaveChanges(object? e)
        {
            if (e is int i)
            {
                lock (locker)
                {
                    Transaction transaction = db.Transactions.Find(UserMainV.Transactions[i].Id);

                    if (transaction != null)
                    {
                        #region Saving changes to DB
                        if (CopyOfList.transactionsBC[i].TimeOfTransaction != UserMainV.Transactions[i].TimeOfTransaction)
                        {
                            transaction.TimeOfTransaction = UserMainV.Transactions[i].TimeOfTransaction;
                        }
                        if (CopyOfList.transactionsBC[i].Summ != UserMainV.Transactions[i].Summ)
                        {
                            transaction.Summ = UserMainV.Transactions[i].Summ;
                        }
                        if (CopyOfList.transactionsBC[i].Type != UserMainV.Transactions[i].Type)
                        {
                            transaction.Type = UserMainV.Transactions[i].Type;
                        }
                        if (CopyOfList.transactionsBC[i].Description != UserMainV.Transactions[i].Description)
                        {
                            transaction.Description = UserMainV.Transactions[i].Description;
                        }
                        db.Transactions.Update(transaction);
                        db.SaveChanges();
                        #endregion
                    }
                    else MessageBox.Show("Transaction == null (couldn't find it in DB or Id == null)");
                }
            }
        }
    }
}

