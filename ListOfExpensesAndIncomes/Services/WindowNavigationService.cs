using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ListOfExpensesAndIncomes.Models;
using ListOfExpensesAndIncomes.Views;
using ListOfExpensesAndIncomes.ViewsModels;

namespace ListOfExpensesAndIncomes.Services
{
    internal class WindowNavigationService : IWindowService
    {
        public void ShowMainWindow(User user, ApplicationContext db)
        {
            MainWindow newWindow = new MainWindow();
            MainWindowViewModel mainVM = new MainWindowViewModel(user, db);
            newWindow.DataContext = mainVM;
            newWindow.Show();

            if (mainVM.CloseAction == null)
                mainVM.CloseAction = new Action(() => newWindow.Close());

            if (mainVM.MinimizeAction == null)
                mainVM.MinimizeAction = new Action(() => newWindow.WindowState = WindowState.Minimized);
        }
    }
}
