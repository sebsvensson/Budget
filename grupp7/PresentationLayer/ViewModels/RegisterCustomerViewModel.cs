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
    public class RegisterCustomerViewModel : BaseViewModel
    {
        private CustomerController customerController;
        private DbAccesEf.MyContext context;

        private string _customID;
        public string CustomID
        {
            get { return _customID; }
            set
            {

              
                _customID = value;
                OnPropertyChanged(null);

            }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                
                _customerName = value;
                OnPropertyChanged(null);
            }
        }


        private ObservableCollection<string> _customerCategories;
        public ObservableCollection<string> CustomerCategories
        {
            get { return _customerCategories; }
            set
            {
                
                _customerCategories = value;
                OnPropertyChanged(null);
            }
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(null);
            }
        }
        public RegisterCustomerViewModel()
        {
            context = new DbAccesEf.MyContext();
            customerController = new CustomerController(context);

            CustomerCategories = new ObservableCollection<string>();

            foreach (CustomerCategory customerCategory in customerController.GetCustomerCategories())
            {
                CustomerCategories.Add(customerCategory.Name);
            }
        }

        private void RegisterCustomer()
        {
            CustomerCategory customerCategory = customerController.GetCustomerCategory(SelectedCategory);
            customerController.RegisterCustomer(CustomID, CustomerName, customerCategory);
        }
        private ICommand _registerCustomerCommand;
        public ICommand RegisterCustomerCommand
        {
            get
            {
                return _registerCustomerCommand ?? (_registerCustomerCommand = new CommandHandler(() => RegisterCustomer()));
            }
        }
    }
}
