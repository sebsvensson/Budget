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
    public class AddRevenueBudgetByCustomerViewModel : BaseViewModel
    {
        private DbAccesEf.MyContext context;
        private ProductController productController;
        private CustomerController customerController;
        private RevenueBudgetController revenueBudgetController;
        public ICommand UpdateViewCommand { get; set; }
        public AddRevenueBudgetByCustomerViewModel()
        {
            context = new DbAccesEf.MyContext();
            productController = new ProductController(context);
            customerController = new CustomerController(context);
            revenueBudgetController = new RevenueBudgetController(context);

            CustomerIDs = new ObservableCollection<string>();
            ProductIDs = new ObservableCollection<string>();

            foreach (Customer customer in customerController.GetAllCustomers())
            {
                CustomerIDs.Add(customer.CustomID);
            }

            foreach (DbAccesEf.Models.Product product in productController.GetAllProducts())
            {
                ProductIDs.Add(product.CustomId);
            }


        }
        private void GetProductInfo(string selectedProductID)
        {
            DbAccesEf.Models.Product product = productController.GetByID(selectedProductID);
            ProductName = product.ProductName;


        }
        private void GetCustomerInfo(string selectedCustomerID)
        {
            Customer customer = customerController.GetByID(selectedCustomerID);
            CustomerName = customer.CustomerName;


        }
        private ICommand _addRevenueBudgetCommand;
        public ICommand AddRevenueBudgetCommand
        {
            get
            {
                
                return _addRevenueBudgetCommand ?? (_addRevenueBudgetCommand = new CommandHandler(() => AddRevenueBudget()));
            }
        }

        private void AddRevenueBudget()
        {
            string gradeA;
            string gradeT;
            if (Safe || Unsafe)
            {
                if (Safe)
                {
                    gradeA = "Säker";
                }
                else
                {
                    gradeA = "Osäker";
                }
                if (Safe)
                {
                    gradeT = "Säker";
                }
                else
                {
                    gradeT = "Osäker";
                }

                Customer customer = customerController.GetByID(SelectedCustomerID);
                DbAccesEf.Models.Product product = productController.GetByID(SelectedProductID);
                revenueBudgetController.AddRevenueBudget(customer.CustomID, customer.CustomerName, product.CustomId, product.ProductName,
                    Agreement, gradeA, Additions, gradeT, Budget, Hours, Comment);
            }
            

        }

        private string _selectedCustomerID;
        public string SelectedCustomerID
        {
            get { return _selectedCustomerID; }
            set
            {
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
        private bool _safe;
        public bool Safe
        {
            get { return _safe; }
            set
            {
                
                _safe = value;
                OnPropertyChanged();
            }
        }
        private bool _unsafe;
        public bool Unsafe
        {
            get { return _unsafe; }
            set
            {
               
                _unsafe = value;
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