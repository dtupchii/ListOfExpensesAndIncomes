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
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace ListOfExpensesAndIncomes.ViewsModels
{
    public class NewDataViewModel : ObservableObject
    {
        private readonly User _user;
        private readonly ApplicationContext _db;
        private Transaction _newTransaction = new();
        private string _selectedItem;
        private RelayCommand _addTransactionCommand;
        private RelayCommand _addHoursCommand, _addMinutesCommand, _minusHoursCommand, _minusMinutesCommand, _stopTimerCommand, _updateSummCommand;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();


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
        public DateTime Today {get; set;} = DateTime.Now.Date;
        public ObservableCollection<string> ItemList { get; set; } = new ObservableCollection<string> { "Income", "Purchase" };
        public string SelectedType
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public NewDataViewModel(User user, ApplicationContext db)
        {
            _db = db;
            _user = user;
            SelectedType = ItemList[0];
            NewTransaction.Type = SelectedType;

            DispatcherTimerSetup();
        }


        #region Commands
        public RelayCommand AddHoursCommand
        {
            get
            {
                return _addHoursCommand ??= new RelayCommand(o =>
                {
                    if (Int32.Parse(NewTransaction.Hours) >= 0 && Int32.Parse(NewTransaction.Hours) <= 22)
                    {
                        NewTransaction.Hours = (Int32.Parse(NewTransaction.Hours)+1).ToString();

                        if (NewTransaction.Hours.Length == 1)
                            NewTransaction.Hours = "0" + NewTransaction.Hours;
                    }
                    else
                        NewTransaction.Hours = "00";
                }, o => true);
            }
        }
        public RelayCommand MinusHoursCommand
        {
            get
            {
                return _minusHoursCommand ??= new RelayCommand(o =>
                {
                    if (Int32.Parse(NewTransaction.Hours) >= 1 && Int32.Parse(NewTransaction.Hours) <= 23)
                    {
                        NewTransaction.Hours = (Int32.Parse(NewTransaction.Hours) - 1).ToString();

                        if (NewTransaction.Hours.Length == 1)
                            NewTransaction.Hours = "0" + NewTransaction.Hours;
                    }
                    else
                        NewTransaction.Hours = "23";
                }, o => true);
            }
        }
        public RelayCommand AddMinutesCommand
        {
            get
            {
                return _addMinutesCommand ??= new RelayCommand(o =>
                {
                    if (Int32.Parse(NewTransaction.Minutes) >= 0 && Int32.Parse(NewTransaction.Minutes) <= 58)
                    {
                        NewTransaction.Minutes = (Int32.Parse(NewTransaction.Minutes) + 1).ToString();

                        if (NewTransaction.Minutes.Length == 1)
                            NewTransaction.Minutes = "0" + NewTransaction.Minutes;
                    }
                    else
                        NewTransaction.Minutes = "00";
                }, o => true);
            }
        }
        public RelayCommand MinusMinutesCommand
        {
            get
            {
                return _minusMinutesCommand ??= new RelayCommand(o =>
                {
                    if (Int32.Parse(NewTransaction.Minutes) >= 1 && Int32.Parse(NewTransaction.Minutes) <= 59)
                    {
                        NewTransaction.Minutes = (Int32.Parse(NewTransaction.Minutes) - 1).ToString();

                        if (NewTransaction.Minutes.Length == 1)
                            NewTransaction.Minutes = "0" + NewTransaction.Minutes;
                    }
                    else
                        NewTransaction.Minutes = "59";
                }, o => true);
            }
        }
        public RelayCommand AddTransactionCommand
        {
            get
            {
                return _addTransactionCommand ??= new RelayCommand(o => AddData(),
                    o => Conditions());
            }
        }
        public RelayCommand StopTimerCommand
        {
            get
            {
                return _stopTimerCommand ??= new RelayCommand(o => dispatcherTimer.Stop(), o => true);
            }
        }
        public RelayCommand UpdateSummCommand
        {
            get
            {
                return _updateSummCommand ??= new RelayCommand(o =>
                {
                    string s = NewTransaction.Summ;
                    NewTransaction.Summ = s;
                }, o => true);
            }
        }
        #endregion


        #region Methods
        private void AddData()
        {
            NewTransaction.Type = SelectedType;
            try
            {
                DateTime timeOfT = new(NewTransaction.Date.Year, NewTransaction.Date.Month, NewTransaction.Date.Day, Int32.Parse(NewTransaction.Hours), Int32.Parse(NewTransaction.Minutes), 0);
                if (NewTransaction.Type == "Purchase" && Double.Parse(NewTransaction.Summ) > 0)
                {
                    NewTransaction.Summ = (Double.Parse(NewTransaction.Summ) * (-1)).ToString();
                }
                string s = String.Format("{0:0.00}", Double.Parse(NewTransaction.Summ));
                Transaction transaction = new() { Type = NewTransaction.Type, TimeOfTransaction = timeOfT, Summ = s, Description = NewTransaction.Description, User = _user, UserId = _user.UserId };

                try
                {
                    _user.Transactions.Add(transaction);
                    _db.Transactions.Add(transaction);
                    _db.SaveChanges();
                    CopyOfList.MakeCopy(_user.Transactions);

                    MessageBox.Show("Item added and saved");

                    SelectedType = "Income";
                    NewTransaction.Date = Today;
                    NewTransaction.Hours = DateTime.Now.Hour.ToString();
                    NewTransaction.Minutes = DateTime.Now.Minute.ToString();
                    NewTransaction.Summ = "";
                    NewTransaction.Description = "";
                    dispatcherTimer.Start();
                }
                catch
                {
                    MessageBox.Show("Couldn't add item to db and save result");
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong!");

            }
        }

        private bool Conditions()
        {
            NewTransaction.Type = SelectedType;
            string summIncomePattern = @"(^[1-9]+(\d*)?(\.)?(\d{1,2})?$)|(^[0-9]?\.\d{1,2}$)|(^0$)",
                   summPurchasePattern = @"(^\-?[1-9]+(\d*)?(\.)?(\d{1,2})?$)|(^\-?[0-9]?\.\d{1,2}$)|(^0$)";

            if (NewTransaction.Type != null && Int32.TryParse(NewTransaction.Hours, out _) && Int32.Parse(NewTransaction.Hours) >= 0 && Int32.Parse(NewTransaction.Hours) < 24 &&
                Int32.TryParse(NewTransaction.Minutes, out _) && Int32.Parse(NewTransaction.Minutes) >= 0 && Int32.Parse(NewTransaction.Minutes) < 60 &&
                ((NewTransaction.Type == "Income" && NewTransaction.Summ.Length > 0 && Regex.IsMatch(NewTransaction.Summ, summIncomePattern)) || 
                (NewTransaction.Type == "Purchase" && NewTransaction.Summ.Length > 0 && Regex.IsMatch(NewTransaction.Summ, summPurchasePattern))) &&
                NewTransaction.Description.Length > 0)
                return true;
            else 
                return false;
        }

        private void DispatcherTimerSetup()
        {
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += new EventHandler(CurrentTimeText);
            dispatcherTimer.Start();
        }

        private void CurrentTimeText(object sender, EventArgs e)
        {
            NewTransaction.Date = DateTime.Today;
            NewTransaction.Hours = DateTime.Now.ToString("HH");
            NewTransaction.Minutes = DateTime.Now.ToString("mm");
        }
        #endregion
    }
}
