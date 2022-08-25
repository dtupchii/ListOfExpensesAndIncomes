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

namespace ListOfExpensesAndIncomes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<Transaction> transactionList { get; set; } = new BindingList<Transaction>();
        private BindingList<Transaction> transactionListBC { get; set; } = new BindingList<Transaction>();
        string? type = "Income";
        ApplicationContext db;
        User user;
        double beginningBalance = 0;
        object locker = new();
        Balance op { get; set; } = new Balance();
        public MainWindow(User user, ApplicationContext db)
        {
            InitializeComponent();

            this.db = db;
            this.user = user;

            beginningBalance = user.BeginningBalance;
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
                db.Transactions?.Where(b => b.UserId == user.UserId).OrderByDescending(c => c.TimeOfTransaction).Load();
                transactionList = db.Transactions.Local.ToBindingList();

                #region Making copy of old list
                foreach (Transaction t in transactionList)
                    if (t != null)
                    {
                        Transaction clone = (Transaction)t.Clone();
                        clone.Id = t.Id;
                        if (clone != null)
                            transactionListBC.Add(clone);
                    }
                #endregion

                balanceText.Text = op.CalculatingBalance(transactionList, beginningBalance).ToString();
            }
            catch
            {
                transactionList = new BindingList<Transaction>();
                transactionListBC = new BindingList<Transaction>();
                MessageBox.Show("Couldn't load data when window loaded");
            }

            #region Output of old list

            //string line = "";

            //for (int i = 0; i < transactionListBC.Count; i++)
            //{
            //    line += transactionListBC[i].TimeOfTransaction.ToString() + " " + transactionListBC[i].Summ.ToString() + " " + transactionListBC[i].Type.ToString() + " " + transactionListBC[i].Description.ToString() + "\n";
            //}
            //MessageBox.Show(line);

            #endregion

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
                    Transaction transaction = new Transaction(dateTime, summ, type, descrField.Text, user);
                    transactionList.Add(transaction);
                    transactionListBC.Add(transaction);
                    try
                    {
                        db.Transactions.Add(transaction);
                        db.SaveChanges();
                        MessageBox.Show("Item added and saved");
                    }
                    catch
                    {
                        MessageBox.Show("Couldn't add item to db and save result");
                    }

                    var sortedListInstance = new BindingList<Transaction>(transactionList.OrderByDescending(x => x.TimeOfTransaction).ToList());
                    transactionList = sortedListInstance;

                    var sortedListInstance1 = new BindingList<Transaction>(transactionListBC.OrderByDescending(x => x.TimeOfTransaction).ToList());
                    transactionListBC = sortedListInstance1;

                    historyGrid.ItemsSource = transactionList;

                    balanceText.Text = op.CalculatingBalance(transactionList, beginningBalance).ToString();

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

        private void SaveChanges(object? e)
        {
            #region Output of old list

            //string line = "";

            //for (int i = 0; i < transactionListBC.Count; i++)
            //{
            //    line += transactionListBC[i].TimeOfTransaction.ToString() + " " + transactionListBC[i].Summ.ToString() + " " + transactionListBC[i].Type.ToString() + " " + transactionListBC[i].Description.ToString() + "\n";
            //}
            //MessageBox.Show(line);

            #endregion

            int idOfChangedItem = 0;

            if (e is string s)
            {
                lock (locker)
                {
                    if ((e == "ItemChanged") && (transactionListBC.Count == transactionList.Count))
                    {
                        for (int i = 0; i < transactionList.Count; i++)
                        {
                            if ((transactionListBC[i].TimeOfTransaction != transactionList[i].TimeOfTransaction) || (transactionListBC[i].Summ != transactionList[i].Summ)
                                || (transactionListBC[i].Type != transactionList[i].Type) || (transactionListBC[i].Description != transactionList[i].Description))
                            {
                                idOfChangedItem = transactionList[i].Id;
                                Transaction? transaction = db.Transactions.Find(idOfChangedItem);

                                if (transaction != null)
                                {
                                    if (transactionListBC[i].TimeOfTransaction != transactionList[i].TimeOfTransaction)
                                    {
                                        transaction.TimeOfTransaction = transactionList[i].TimeOfTransaction;
                                        db.Transactions.Update(transaction);
                                        db.SaveChanges();
                                        MessageBox.Show("Item's time was changed in DB");
                                    }
                                    if (transactionListBC[i].Summ != transactionList[i].Summ)
                                    {
                                        transaction.Summ = transactionList[i].Summ;
                                        db.Transactions.Update(transaction);
                                        db.SaveChanges();
                                        MessageBox.Show("Item's summ was changed in DB");
                                    }
                                    if (transactionListBC[i].Type != transactionList[i].Type)
                                    {
                                        transaction.Type = transactionList[i].Type;
                                        db.Transactions.Update(transaction);
                                        db.SaveChanges();
                                        MessageBox.Show("Item's type was changed in DB");
                                    }
                                    if (transactionListBC[i].Description != transactionList[i].Description)
                                    {
                                        transaction.Description = transactionList[i].Description;
                                        db.Transactions.Update(transaction);
                                        db.SaveChanges();
                                        MessageBox.Show("Item's description was changed in DB");
                                    }
                                }
                                else
                                    MessageBox.Show("Couldn't change item and save result");
                            }
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
                {
                    var sortedListInstance = new BindingList<Transaction>(transactionList.OrderByDescending(x => x.TimeOfTransaction).ToList());
                    transactionList = sortedListInstance;

                    if (e.ListChangedType == ListChangedType.ItemChanged)
                    {
                        Thread mainThread = Thread.CurrentThread;
                        Thread thread = new Thread(SaveChanges);
                        thread.Start("ItemChanged");
                        thread.Join();
                    }
                }

                balanceText.Text = op.CalculatingBalance(transactionList, beginningBalance).ToString();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Transaction t = (Transaction)historyGrid.SelectedItem;
            transactionList.Remove(t);
            db.Transactions.Remove(t);
            db.SaveChanges();
        }
    }
}