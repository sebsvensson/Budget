using BusinessLogic.Controllers;
using DbAccesEf.Models;
using PresentationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.ViewModels
{
    public class RevenueBudgetByProductViewModel : BaseViewModel
    {
        private DbAccesEf.MyContext context;
        private ProductController productController;
        private CustomerController customerController;
        private RevenueBudgetController revenueBudgetController;
        private BudgetLockController budgetLockController;

        private MainViewModel mainViewModel;
        public ICommand UpdateViewCommand { get; set; }

        public RevenueBudgetByProductViewModel(MainViewModel mainViewModel)
        {
            context = new DbAccesEf.MyContext();
            revenueBudgetController = new RevenueBudgetController(context);
            productController = new ProductController(context);
            customerController = new CustomerController(context);
            budgetLockController = new BudgetLockController(context);

            this.mainViewModel = mainViewModel;
            UpdateViewCommand = new UpdateViewCommand(this.mainViewModel);
            ProductIDs = new ObservableCollection<string>();

            foreach (Product product in productController.GetAllProducts())
            {
                ProductIDs.Add(product.CustomId);
            }

            //Disable buttons based on budgetlock
            RevenueBudgetLocked = budgetLockController.GetRevenueBudgetLocked();
        }

        public void ShowBudgets(string selectedProductID)
        {
            IEnumerable<RevenueBudget> revenueBudgets = revenueBudgetController.GetProductBudgets(selectedProductID);

            RevenueBudgets = new ObservableCollection<RevenueBudget>();

            foreach (RevenueBudget revenueBudget in revenueBudgets)
            {
                ProductName = revenueBudget.Product.ProductName;
                RevenueBudgets.Add(revenueBudget);
                AgreementSum += revenueBudget.Agreement;
                BudgetSum += revenueBudget.Budget;
                HoursSum += revenueBudget.Hours;
                AdditionsSum += revenueBudget.Additions;
            }

        }

        public void RemoveBudget(RevenueBudget revenueBudget, string selectedProductID)
        {
            revenueBudgetController.RemoveRevenueBudget(revenueBudget);
            ShowBudgets(selectedProductID);
        }

        private ICommand _removeRevenueBudgetCommand;
        public ICommand RemoveRevenueBudgetCommand
        {
            get
            {
                return _removeRevenueBudgetCommand ?? (_removeRevenueBudgetCommand = new CommandHandler(() => RemoveBudget(SelectedRevenueBudget, SelectedProductID)));
            }
        }

        private RevenueBudget _selectedRevenueBudget;
        public RevenueBudget SelectedRevenueBudget
        {
            get { return _selectedRevenueBudget; }
            set
            {
                _selectedRevenueBudget = value;
                OnPropertyChanged();
            }
        }

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RevenueBudget> _revenueBudgets;
        public ObservableCollection<RevenueBudget> RevenueBudgets
        {
            get { return _revenueBudgets; }
            set
            {
                _revenueBudgets = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _productIDs;
        public ObservableCollection<string> ProductIDs
        {
            get { return _productIDs; }
            set
            {
                _productIDs = value;
                OnPropertyChanged();
            }
        }

        private string _selectedproductID;
        public string SelectedProductID
        {
            get { return _selectedproductID; }
            set
            {
                ShowBudgets(value);
                _selectedproductID = value;
                OnPropertyChanged();
            }
        }


        private double _agreementSum = 0;
        public double AgreementSum
        {
            get { return _agreementSum; }
            set
            {
                _agreementSum = value;
                OnPropertyChanged();
            }
        }

        private double _budgetSum = 0;
        public double BudgetSum
        {
            get { return _budgetSum; }
            set
            {
                _budgetSum = value;
                OnPropertyChanged();
            }
        }

        private double _hoursSum = 0;
        public double HoursSum
        {
            get { return _hoursSum; }
            set
            {
                _hoursSum = value;
                OnPropertyChanged();
            }
        }

        private double _additionsSum = 0;
        public double AdditionsSum
        {
            get { return _additionsSum; }
            set
            {
                _additionsSum = value;
                OnPropertyChanged();
            }
        }

        private bool _revenueBudgetLocked;
        public bool RevenueBudgetLocked
        {
            get { return _revenueBudgetLocked; }
            set
            {
                _revenueBudgetLocked = value;
                OnPropertyChanged(null);
            }
        }
    }
}
