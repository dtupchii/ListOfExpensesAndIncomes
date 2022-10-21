using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfExpensesAndIncomes.Core;

namespace ListOfExpensesAndIncomes.Services
{
    public class InitializingViewVisibility : ObservableObject
    {
        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                OnPropertyChanged();
            }
        }
    }
}
