using ListOfExpensesAndIncomes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfExpensesAndIncomes.Services;
using ListOfExpensesAndIncomes.Models;

namespace ListOfExpensesAndIncomes.ViewsModels.InitializingWindow
{
    internal class InitializingWindowViewModel : ObservableObject
    {
        private object _currentView;
        public InitializingViewVisibility _isViewVisible = new InitializingViewVisibility();
        private readonly ApplicationContext db = new ApplicationContext();

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public InitializingViewVisibility IsViewVisible
        {
            get { return _isViewVisible; }
            set 
            { 
                _isViewVisible = value;
                OnPropertyChanged();
            }
        }
        public Action CloseAction { get; set; }
        public Action MinimizeAction { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand MinimizeCommand { get; set; }
        public RelayCommand LoginViewCommand { get; set; }
        public RelayCommand RegistrationViewCommand { get; set; }
        public LoginViewModel LoginVM { get; set; }
        public RegistrationViewModel RegistrationVM { get; set; }


        public InitializingWindowViewModel()
        {
            IsViewVisible.Visible = true;
            LoginVM = new LoginViewModel(db, IsViewVisible);
            RegistrationVM = new RegistrationViewModel(db, IsViewVisible);
            CurrentView = LoginVM;

            CloseCommand = new RelayCommand(o => CloseAction());
            MinimizeCommand = new RelayCommand(o =>MinimizeAction());

            LoginViewCommand = new RelayCommand(o =>
            {
                CurrentView = LoginVM;
            });
            RegistrationViewCommand = new RelayCommand(o =>
            {
                CurrentView = RegistrationVM;
            });
        }

    }
}
