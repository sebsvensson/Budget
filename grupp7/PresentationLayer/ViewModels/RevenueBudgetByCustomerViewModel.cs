using BusinessLogic.Controllers;
using DbAccesEf;
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
    public class RevenueBudgetByCustomerViewModel : BaseViewModel
    {
        private DbAccesEf.MyContext context;
        private ProductController productController;
        private CustomerController customerController;
        private RevenueBudgetController revenueBudgetController;

        private MainViewModel mainViewModel;
        public ICommand UpdateViewCommand { get; set; }
        public RevenueBudgetByCustomerViewModel(MainViewModel mainViewModel)
        {
            context = new DbAccesEf.MyContext();
            revenueBudgetController = new RevenueBudgetController(context);
            productController = new ProductController(context);
            customerController = new CustomerController(context);
            this.mainViewModel = mainViewModel;
            UpdateViewCommand = new UpdateViewCommand(this.mainViewModel);

            CustomerIDs = new ObservableCollection<string>();

            foreach (Customer customer in customerController.GetAllCustomers())
            {
                CustomerIDs.Add(customer.CustomID);
            }
           
            this.mainViewModel = mainViewModel;
            UpdateViewCommand = new UpdateViewCommand(this.mainViewModel);

        }
        private void GetProductInfo(string selectedProductID)
        {
            DbAccesEf.Models.Product product = productController.GetByID(selectedProductID);
            ProductName = product.ProductName;


        }

        public void ShowBudgets(string selectedCustomerID)
        {
            IEnumerable<RevenueBudget> revenueBudgets = revenueBudgetController.GetCustomerBudgets(selectedCustomerID);

            RevenueBudgets = new ObservableCollection<RevenueBudget>();

            foreach (RevenueBudget revenueBudget in revenueBudgets)
            {
                CustomerName = revenueBudget.Customer.CustomerName;
                ProductID = revenueBudget.Product.CustomId;
                Agreement = revenueBudget.Agreement;
                GradeA = revenueBudget.Grade_A;
                Additions = revenueBudget.Additions;
                GradeT = revenueBudget.Grade_T;
                Budget = revenueBudget.Budget;
                Hours = revenueBudget.Hours;
                Comment = revenueBudget.Comment;

                RevenueBudgets.Add(revenueBudget);
            }
        }

        private void GetCustomerInfo(string selectedCustomerID)
        {
            Customer customer = customerController.GetByID(selectedCustomerID);
            CustomerName = customer.CustomerName;


        }

        private string _selectedCustomerID;
        public string SelectedCustomerID
        {
            get { return _selectedCustomerID; }
            set
            {
                ShowBudgets(value);
                GetCustomerInfo(value);
                _selectedCustomerID = value;
                OnPropertyChanged();
            }
        }
        private string _customerID;
        public string CustomerID
        {
            get { return _customerID; }
            set
            {
                _customerID = value;
                OnPropertyChanged();
            }
        }
        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
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
        private ObservableCollection<string> _customerIDs;
        public ObservableCollection<string> CustomerIDs
        {
            get { return _customerIDs; }
            set
            {
                _customerIDs = value;
                OnPropertyChanged();
            }
        }
        private string _selectedproductID;
        public string SelectedProductID
        {
            get { return _selectedproductID; }
            set
            {
                GetProductInfo(value);
                _selectedproductID = value;
                OnPropertyChanged();
            }
        }

        private string _productID;
        public string ProductID
        {
            get { return _productID; }
            set
            {
               
                _productID = value;
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
        private int _agreement;
        public int Agreement
        {
            get { return _agreement; }
            set
            {
                _agreement = value;
                OnPropertyChanged();
            }
        }
        private string _gradeA;
        public string GradeA
        {
            get { return _gradeA; }
            set
            {
                _gradeA = value;
                OnPropertyChanged();
            }
        }
        private string _gradeT;
        public string GradeT
        {
            get { return _gradeT; }
            set
            {
                _gradeT = value;
                OnPropertyChanged();
            }
        }
        private int _additions;
        public int Additions
        {
            get { return _additions; }
            set
            {
                _additions = value;
                OnPropertyChanged();
            }
        }
        private int _budget;
        public int Budget
        {
            get { return _budget; }
            set
            {
                _budget = value;
                OnPropertyChanged();
            }
        }
        private int _hours;
        public int Hours
        {
            get { return _hours; }
            set
            {
                _hours = value;
                OnPropertyChanged();
            }
        }
        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }

        




    }
}
