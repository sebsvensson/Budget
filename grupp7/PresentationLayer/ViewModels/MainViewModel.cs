using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresentationLayer.Commands;
using PresentationLayer.ViewModels;

namespace PresentationLayer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        //Displays selected view next to menu
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        //Colors
        private string _menuColor = Colors.Menu;
        public string MenuColor
        {
            get { return _menuColor; }
            set
            {
                _menuColor = value;
                OnPropertyChanged(null);
            }
        }

        private string _menuSelectedColor = Colors.MenuSelected;
        public string MenuSelectedColor
        {
            get { return _menuSelectedColor; }
            set
            {
                _menuSelectedColor = value;
                OnPropertyChanged(null);
            }
        }

        private string _textSelectedColor = Colors.TextSelected;
        public string TextSelectedColor
        {
            get { return _textSelectedColor; }
            set
            {
                _textSelectedColor = value;
                OnPropertyChanged(null);
            }
        }

        //Command to change views
        public ICommand UpdateViewCommand { get; set; }

        //Sets columnspan of menu to minimize menu
        private int _columnSpan = 2;
        public int ColumnSpan
        {
            get { return _columnSpan; }
            set
            {
                _columnSpan = value;
                OnPropertyChanged(null);
            }
        }

        //minimize menu
        private int _gridRow = 0;
        public int GridRow
        {
            get { return _gridRow; }
            set
            {
                _gridRow = value;
                OnPropertyChanged(null);
            }
        }

        //minimize menu
        private int _gridColumn = 0;
        public int GridColumn
        {
            get { return _gridColumn; }
            set
            {
                _gridColumn = value;
                OnPropertyChanged(null);
            }
        }

        //menu width
        private int _menuWidth = 150;
        public int MenuWidth
        {
            get { return _menuWidth; }
            set
            {
                _menuWidth = value;
                OnPropertyChanged(null);
            }
        }

        //SUB MENU HEIGHTS

        //budget
        private bool _isCheckedBudget;
        public bool IsCheckedBudget
        {
            get { return _isCheckedBudget; }
            set
            {
                _isCheckedBudget = value;
                OnPropertyChanged(null);

                if (value == false)
                {
                    BudgetMenuHeight = 0;
                }
                else
                {
                    BudgetMenuHeight = 40;
                }
            }
        }

        private int _budgetMenuHeight = 0;
        public int BudgetMenuHeight
        {
            get { return _budgetMenuHeight; }
            set
            {
                _budgetMenuHeight = value;
                OnPropertyChanged(null);
            }
        }

        //product
        private bool _isCheckedProduct;
        public bool IsCheckedProduct
        {
            get { return _isCheckedProduct; }
            set
            {
                _isCheckedProduct = value;
                OnPropertyChanged(null);

                if (value == false)
                {
                    ProductMenuHeight = 0;
                }
                else
                {
                    ProductMenuHeight = 40;
                }
            }
        }

        private int _ProductMenuHeight = 0;
        public int ProductMenuHeight
        {
            get { return _ProductMenuHeight; }
            set
            {
                _ProductMenuHeight = value;
                OnPropertyChanged(null);
            }
        }

        //customer
        private bool _isCheckedCustomer;
        public bool IsCheckedCustomer
        {
            get { return _isCheckedCustomer; }
            set
            {
                _isCheckedCustomer = value;
                OnPropertyChanged(null);

                if (value == false)
                {
                    CustomerMenuHeight = 0;
                }
                else
                {
                    CustomerMenuHeight = 40;
                }
            }
        }

        private int _CustomerMenuHeight = 0;
        public int CustomerMenuHeight
        {
            get { return _CustomerMenuHeight; }
            set
            {
                _CustomerMenuHeight = value;
                OnPropertyChanged(null);
            }
        }

        //if menu is minimized or not
        private bool menuWide = true;

        private ICommand _menuDropCommand;
        public ICommand MenuDropCommand
        {
            get
            {
                return _menuDropCommand ?? (_menuDropCommand = new CommandHandler(() => MenuDrop()));
            }
        }

        //drops the menu
        private void MenuDrop()
        {
            if (menuWide)
            {
                MenuWidth = 15;
                menuWide = false;
            }
            else
            {
                MenuWidth = 150;
                menuWide = true;
            }
        }

        //Logs out the user, changes grids to fullscreen
        private ICommand _logOutCommand;
        public ICommand LogOutCommand
        {
            get
            {
                return _logOutCommand ?? (_logOutCommand = new CommandHandler(() => LogOut()));
            }
        }
        private void LogOut()
        {
            SelectedViewModel = new LoginViewModel(this);
            ColumnSpan = 2;
            GridColumn = 0;
            GridRow = 0;

        }

        public MainViewModel()
        {
            //Set SelectedViewModel to startup UserControl here (Login view probably)
            //SelectedViewModel = new TestViewModel();
            SelectedViewModel = new LoginViewModel(this);

            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
