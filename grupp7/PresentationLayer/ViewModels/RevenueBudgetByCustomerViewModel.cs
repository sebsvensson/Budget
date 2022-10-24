using BusinessLogic.Controllers;
using DbAccesEf;
using DbAccesEf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class RevenueBudgetByCustomerViewModel : BaseViewModel
    {
        private RevenueBudgetController revenueBudgetController;
        private MyContext context;
        public RevenueBudgetByCustomerViewModel()
        {
            context = new DbAccesEf.MyContext();
            revenueBudgetController = new RevenueBudgetController(context);

            foreach (RevenueBudget revenueBudget in revenueBudgetController.GetAllRevenueBudgets())
            {
                RevenueBudgetsByCustomer.Add(revenueBudget);
            }
        }
        private ObservableCollection<RevenueBudget> _revenueBudgetByCustomer { get; set;}
        public ObservableCollection<RevenueBudget> RevenueBudgetsByCustomer
        {
            get { return _revenueBudgetByCustomer; }
            set
            {
                _revenueBudgetByCustomer = value;
                OnPropertyChanged();
            }
            
        }
        private string _selectedcustomerID;
        public string SelectedCustomerID
        {
            get { return _customerID; }
            set
            {
                _customerID = value;
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

        private void GetAllRevenueBudgets(string selectedCustomID)
        {
            RevenueBudget revenueBudgetCustomer = revenueBudgetController.GetByCustomerID(selectedCustomID);
        }


    }
}
