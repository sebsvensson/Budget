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
    public class EditCustomerViewModel : BaseViewModel
    {
        private CustomerController customerController;
        private DbAccesEf.MyContext context;

        public EditCustomerViewModel()
        {
            context = new DbAccesEf.MyContext();
            customerController = new CustomerController(context);
            CustomIDs = new ObservableCollection<string>();
            CustomerCategories = new ObservableCollection<string>();

            foreach (Customer customer in customerController.GetAllCustomers())
            {
                CustomIDs.Add(customer.CustomID);
            }
            foreach (CustomerCategory customerCategory in customerController.GetCustomerCategories())
            {
                CustomerCategories.Add(customerCategory.Name);
            }
        }

        private ObservableCollection<string> _customIDs;
        public ObservableCollection<string> CustomIDs
        {
            get { return _customIDs; }
            set
            {
                OnPropertyChanged(null);
                _customIDs = value;
            }
        }
        private ObservableCollection<string> _customerCategories;
        public ObservableCollection<string> CustomerCategories
        {
            get { return _customerCategories; }
            set
            {
                OnPropertyChanged(null);
                _customerCategories = value;
            }
        }

        private string _customID;
        public string CustomID
        {
            get { return _customID; }
            set
            {
                OnPropertyChanged(null);
                _customID = value;
            }
        }

        private string _selectedCustomID;
        public string SelectedCustomID
        {
            get { return _selectedCustomID; }
            set
            {
                GetCustomerInfo(value);
                OnPropertyChanged(null);
                _selectedCustomID = value;
            }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                OnPropertyChanged(null);
                _customerName = value;
            }
        }

        private string _selectedCustomerName;
        public string SelectedCustomerName
        {
            get { return _selectedCustomerName; }
            set
            {
                OnPropertyChanged(null);
                _selectedCustomerName = value;
            }
        }

        private string _customerCategory;
        public string CustomerCategory
        {
            get { return _customerCategory; }
            set
            {
                OnPropertyChanged(null);
                _customerCategory = value;
            }
        }

        private string _selectedCustomerCategory;
        public string SelectedCustomerCategory
        {
            get { return _selectedCustomerCategory; }
            set
            {
                OnPropertyChanged(null);
                _selectedCustomerCategory = value;
            }
        }

        public void GetCustomerInfo(string selectedCustomID)
        {
            DbAccesEf.Models.Customer customer = customerController.GetByID(selectedCustomID);
            
            CustomerName = customer.CustomerName;
            CustomerCategory = customer.Category.Name;
        }

        private ICommand _editCustomerCommand;
        public ICommand EditCustomerCommand
        {
            get
            {
                return _editCustomerCommand ?? (_editCustomerCommand = new CommandHandler(() => EditCustomer()));
            }
        }

        public void EditCustomer()
        {
            customerController.EditCustomer(SelectedCustomID, CustomerName, CustomerCategory);

        }

    }
}
