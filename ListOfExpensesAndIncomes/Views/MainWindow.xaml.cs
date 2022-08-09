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

namespace ListOfExpensesAndIncomes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<Transaction> transactionList { get; set; } = new BindingList<Transaction>();
        private BindingList<Transaction> transactionListBC { get; set; }
        string type = "Income";
        ApplicationContext db;
        User user;
        double beginningBalance = 0;
        Balance op { get; set; } = new Balance();
        public MainWindow(User user)
        {
            InitializeComponent();

            this.user = user;
            beginningBalance = user.BeginningBalance;
            db = new ApplicationContext();
            try
            {
                db.Transactions.Where(b => b.UserId == user.UserId).OrderByDescending(c => c.TimeOfTransaction).Load();
                transactionList = db.Transactions.Local.ToBindingList();
            }
            catch
            {
                transactionList = new BindingList<Transaction>();
                //transactionListBC = new BindingList<Transaction>();
            }

            dateField.DisplayDateEnd = DateTime.Now;
        }

        private void typeField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cB = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cB.SelectedItem;
            type = item.Content.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                balanceText.Text = op.CalculatingBalance(transactionList, beginningBalance).ToString();
            }
            catch
            {
                MessageBox.Show("Couldn't load data when window loaded");
            }

            /* #region Вывод старого списка

             string line = "";

             for (int i = 0; i < transactionListBC.Count; i++)
             {
                 line += transactionListBC[i].TimeOfTransaction.ToString() + " " + transactionListBC[i].Summ.ToString() + " " + transactionListBC[i].Type.ToString() + " " + transactionListBC[i].Description.ToString() + "\n";
             }
             MessageBox.Show(line);
             #endregion*/

            historyGrid.ItemsSource = transactionList;
            transactionList.ListChanged += _transactionList_ListChanged;
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dateTime = Convert.ToDateTime(dateField.Text + " " + timeField.Text);
                double summ = Convert.ToDouble(sumField.Text);

                if (summ > 0)
                {
                    Transaction transaction = new Transaction(dateTime, summ, type, descrField.Text, user.UserId);
                    transactionList.Add(transaction);
                    db.Transactions.Add(transaction);
                    db.SaveChanges();

                    var sortedListInstance = new BindingList<Transaction>(transactionList.OrderByDescending(x => x.TimeOfTransaction).ToList());
                    transactionList = sortedListInstance;

                    balanceText.Text = op.CalculatingBalance(transactionList, beginningBalance).ToString();

                    historyGrid.ItemsSource = transactionList;

                    dateField.Text = "";
                    timeField.Text = "";
                    descrField.Text = "";
                    IncomeComboBoxItem.IsSelected = true;
                }
                else
                    MessageBox.Show("Summ should be positive");
                sumField.Text = "";
            }
            catch
            {
                MessageBox.Show("Something went wrong!");
            }
        }

        private void SaveChanges(BindingList<Transaction> oldL, BindingList<Transaction> newL, ListChangedEventArgs e)
        {
            string line = "";

            for (int i = 0; i < oldL.Count; i++)
            {
                line += oldL[i].TimeOfTransaction.ToString() + " " + oldL[i].Summ.ToString() + " " + oldL[i].Type.ToString() + " " + oldL[i].Description.ToString() + "\n";
            }
            MessageBox.Show(line);


            int idOfChangedItem = 0;

            using (var db = new ApplicationContext())
            {
                if (e.ListChangedType == ListChangedType.ItemDeleted)
                {
                    for (int i = 0; i < newL.Count; i++)
                    {
                        if (oldL[i] != newL[i])
                        {
                            idOfChangedItem = oldL[i].Id;
                            var transaction = db.Transactions.Find(idOfChangedItem);
                            db.Transactions.Remove(transaction);
                            db.SaveChanges();
                        }
                    }
                }
                else if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    for (int i = 0; i < newL.Count; i++)
                    {
                        if (oldL[i] != newL[i])
                        {
                            idOfChangedItem = newL[i].Id;
                            var transaction = db.Transactions.Find(idOfChangedItem);

                            if (oldL[i].TimeOfTransaction != newL[i].TimeOfTransaction)
                            {
                                transaction.TimeOfTransaction = newL[i].TimeOfTransaction;
                            }
                            if (oldL[i].Summ != newL[i].Summ)
                            {
                                transaction.Summ = newL[i].Summ;
                            }
                            if (oldL[i].Type != newL[i].Type)
                            {
                                transaction.Type = newL[i].Type;
                            }
                            if (oldL[i].Description != newL[i].Description)
                            {
                                transaction.Description = newL[i].Description;
                            }
                            db.SaveChanges();
                        }
                    }
                }
                else if (e.ListChangedType == ListChangedType.ItemAdded)
                {
                    for (int i = 0; i < newL.Count; i++)
                    {
                        if (oldL[i] != newL[i])
                        {
                            idOfChangedItem = oldL[i].Id;
                            var transaction = db.Transactions.Find(idOfChangedItem);
                            db.Transactions.Add(transaction);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
        private void _transactionList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (transactionList.Count > 0)
                    balanceText.Text = op.CalculatingBalance(transactionList, beginningBalance).ToString();
                //SaveChanges(transactionListBC, transactionList, e);
            }
        }
    }
}
