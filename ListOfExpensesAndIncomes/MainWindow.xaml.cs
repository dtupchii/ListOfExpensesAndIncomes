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
        private BindingList<TransactionModel> transactionList = new BindingList<TransactionModel>();
        private readonly string _listPath = "list.json";
        private readonly string _balancePath = "balance.json";
        string type = "Income";
        private FileIO _file;
        Balance op = new Balance();

        public MainWindow()
        {
            InitializeComponent();
            _file = new FileIO(_listPath, _balancePath);

            dateField.DisplayDateEnd = DateTime.Now;
            
        }
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dateTime = Convert.ToDateTime(dateField.Text + " " + timeField.Text);
                double summ = Convert.ToDouble(sumField.Text);

                if (summ > 0)
                {
                    transactionList.Add(new TransactionModel(dateTime, summ, type, descrField.Text));
                                        
                    var sortedListInstance = new BindingList<TransactionModel>(transactionList.OrderByDescending(x => x.TimeOfTransaction).ToList());
                    transactionList = sortedListInstance;

                    balanceText.Text = op.CalculatingBalance(transactionList).ToString();

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
                MessageBox.Show("Something went wrong!");
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                transactionList = _file.ReadData();
                balanceText.Text = op.CalculatingBalance(transactionList).ToString();
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
                    balanceText.Text = op.CalculatingBalance(transactionList).ToString();
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
