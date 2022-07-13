using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ListOfExpensesAndIncomes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<TransactionsModel> transactionList = new BindingList<TransactionsModel>();
        private readonly string _listPath = "list.json";
        private readonly string _balancePath = "balance.json";
        private FileIO _file;
        string type = "Income";
        public double CurrentBalance { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            dateField.DisplayDateEnd = DateTime.Now;
            
        }
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _file = new FileIO(_listPath, _balancePath);
                DateTime dateTime = Convert.ToDateTime(dateField.Text + " " + timeField.Text);
                double summ = Convert.ToDouble(sumField.Text);

                if (summ > 0)
                {
                    transactionList.Add(new TransactionsModel(dateTime, summ, type, descrField.Text));
                                        
                    var sortedListInstance = new BindingList<TransactionsModel>(transactionList.OrderByDescending(x => x.TimeOfTransaction).ToList());
                    transactionList = sortedListInstance;

                    CurrentBalance = Operations.CalculatingBalance(transactionList);
                   
                    _file.SaveData(transactionList);
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
                MessageBox.Show("Enter data!");
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _file = new FileIO(_listPath, _balancePath);
                transactionList = _file.ReadData();
            }
            catch
            {
                MessageBox.Show("Couldn't load data when window loaded");
            }            

            historyGrid.ItemsSource = transactionList;
            transactionList.ListChanged += _transactionList_ListChanged;
        }

        private void _transactionList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (transactionList.Count > 0)
                    CurrentBalance = Operations.CalculatingBalance(transactionList);
                _file = new FileIO(_listPath, _balancePath);
                _file.SaveData(transactionList);
            }
        }

        private void typeField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cB = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cB.SelectedItem;
            type = item.Content.ToString();
        }
    }
}
