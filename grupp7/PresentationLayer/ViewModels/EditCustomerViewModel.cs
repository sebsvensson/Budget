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
       
        private string _customId;
        public string CustomId
        {
            get { return _customId; }
            set
            {
                GetCustomerInfo(value);
                OnPropertyChanged(null);
                _customId = value;
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
        private ObservableCollection<string> _customerIDs;
        public ObservableCollection<string> CustomerIDs
        {
            get {  return _customerIDs; }
            set
            {
                OnPropertyChanged(null);
                _customerIDs = value;
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
        public EditCustomerViewModel()
        {
            context = new DbAccesEf.MyContext();
            customerController = new CustomerController(context);

            CustomerIDs = new ObservableCollection<string>();
            CustomerCategories = new ObservableCollection<string>();

            foreach (Customer customer in customerController.GetAllCustomers())
            {
                CustomerIDs.Add(customer.CustomID);
            }
            foreach (CustomerCategory customerCategory in customerController.GetCustomerCategories())
            {
                CustomerCategories.Add(customerCategory.Name);
            }
        }

        public void GetCustomerInfo(string selectedID)
        {
            Customer customer = customerController.GetByID(selectedID);
            customer.CustomID = CustomId;
            customer.CustomerName = CustomerName;
            customer.Category.Name = CustomerCategory;
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
            customerController.EditCustomer(CustomId, CustomerName, CustomerCategory);
        }

    }
}
