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
using System.Windows.Shapes;
using ListOfExpensesAndIncomes.ViewsModels.InitializingWindow;

namespace ListOfExpensesAndIncomes.Views.InitializingWindow
{
    /// <summary>
    /// Interaction logic for InitializingWindowView.xaml
    /// </summary>
    public partial class InitializingWindowView : Window
    {
        public InitializingWindowView()
        {
            InitializeComponent();
            InitializingWindowViewModel iwVM = new InitializingWindowViewModel();
            this.DataContext = iwVM;

            if (iwVM.CloseAction == null)
             iwVM.CloseAction = new Action(() => this.Close());

            if (iwVM.MinimizeAction == null)
                iwVM.MinimizeAction = new Action(() => this.WindowState = WindowState.Minimized);
        }
    }
}
