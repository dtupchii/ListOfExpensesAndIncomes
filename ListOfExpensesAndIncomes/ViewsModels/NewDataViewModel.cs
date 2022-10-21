using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ListOfExpensesAndIncomes.Core;
using ListOfExpensesAndIncomes.Models;
using ListOfExpensesAndIncomes.Services;

namespace ListOfExpensesAndIncomes.ViewsModels
{
    public class NewDataViewModel : ObservableObject
    {
        private User _user;
        private ApplicationContext _db;
        private Transaction _newTransaction = new Transaction();
        private RelayCommand _addTransactionCommand;
        private string _amount;
        private DateOnly _selectedDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        public NewDataViewModel(User user, ApplicationContext db)
        {
            _db = db;
            _user = user;

        }

        #region Properties
        public Transaction NewTransaction
        {
            get { return _newTransaction; }
            set
            {
                _newTransaction = value;
                OnPropertyChanged();
            }
        }
        public DateOnly SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }
        public DateTime Today {get; set;} = DateTime.Now.Date;
        #endregion

        #region Commands
        public RelayCommand AddTransactionCommand
        {
            get
            {
                return _addTransactionCommand ??
                    (_addTransactionCommand = new RelayCommand(o => AddData(), o => true));
            }
        }
        #endregion

        #region Methods
        private void AddData()
        {
            try
            {
                if (NewTransaction.Summ > 0)
                {
                    if (NewTransaction.Type == "System.Windows.Controls.ComboBoxItem: Income")
                        NewTransaction.Type = "Income";
                    else if (NewTransaction.Type == "System.Windows.Controls.ComboBoxItem: Purchase")
                        NewTransaction.Type = "Purchase";
                    else
                        MessageBox.Show("Type is not selected!");

                    Transaction transaction = new Transaction { Type = NewTransaction.Type, TimeOfTransaction = NewTransaction.TimeOfTransaction, Summ = NewTransaction.Summ, Description = NewTransaction.Description, User = _user, UserId = _user.UserId};
                    
                    try
                    {
                        _user.Transactions.Add(transaction);
                        _db.Transactions.Add(transaction);
                        _db.SaveChanges();
                        CopyOfList.MakeCopy(_user.Transactions);

                        MessageBox.Show("Item added and saved");

                        NewTransaction.Type = "Income";
                        NewTransaction.TimeOfTransaction = Today;
                        NewTransaction.Summ = 0;
                        NewTransaction.Description = "";
                    }
                    catch
                    {
                        MessageBox.Show("Couldn't add item to db and save result");
                    }                    
                }
                else 
                    MessageBox.Show("Summ should be positive");
            }
            catch
            {
                MessageBox.Show("Something went wrong!");
            }
        }
        #endregion
    }
}
