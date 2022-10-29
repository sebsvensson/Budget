using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic.Controllers;
using PresentationLayer.Commands;
using DbAccesEf;
using DbAccesEf.Models;

namespace PresentationLayer.ViewModels
{
    public class BudgetResultViewModel : BaseViewModel
    {
        private BudgetResultController budgetResultController;
        private ProductController productController;

        private double _revenue;
        public double Revenue
        {
            get { return _revenue; }
            set
            {
                _revenue = value;
                OnPropertyChanged(null);
            }
        }
        private double _expense;
        public double Expense
        {
            get { return _expense; }
            set
            {
                _expense = value;
                OnPropertyChanged(null);
            }
        }
        private double _result;
        public double Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged(null);
            }
        }

        private bool _productBool;
        public bool ProductBool
        {
            get { return _productBool; }
            set
            {
                _productBool = value;
                OnPropertyChanged(null);
                SetComboBoxItemSource();
            }
        }
        private bool _productGroupBool;
        public bool ProductGroupBool
        {
            get { return _productGroupBool; }
            set
            {
                _productGroupBool = value;
                OnPropertyChanged(null);
                SetComboBoxItemSource();
            }
        }
        private bool _departmentBool;
        public bool DepartmentBool
        {
            get { return _departmentBool; }
            set
            {
                _departmentBool = value;
                OnPropertyChanged(null);
                SetComboBoxItemSource();
            }
        }
        private bool _officeBool;
        public bool OfficeBool
        {
            get { return _officeBool; }
            set
            {
                _officeBool = value;
                OnPropertyChanged(null);
                SetComboBoxItemSource();
            }
        }

        private ObservableCollection<string> _comboboxSource;
        public ObservableCollection<string> ComboboxSource
        {
            get { return _comboboxSource; }
            set
            {
                _comboboxSource = value;
                OnPropertyChanged();
            }
        }

        private string _comboboxSelected;
        public string ComboboxSelected
        {
            get { return _comboboxSelected; }
            set
            {
                _comboboxSelected = value;
                OnPropertyChanged(null);
            }
        }

        private ICommand _readCommand;
        public ICommand ReadCommand
        {
            get
            {
                return _readCommand ?? (_readCommand = new CommandHandler(() => Read()));
            }
        }

        public BudgetResultViewModel()
        {
            MyContext context = new MyContext();
            budgetResultController = new BudgetResultController(context);
            productController = new ProductController(context);
        }

        private void Read()
        {
            SetResult();
        }

        private void SetComboBoxItemSource()
        {

            System.Diagnostics.Debug.WriteLine("test");

            if (ProductBool)
            {
                ComboboxSource = new ObservableCollection<string>();

                foreach(Product p in productController.GetAll().ToList())
                {
                    ComboboxSource.Add(p.ProductName);
                }
            }
            else if (ProductGroupBool)
            {
                ComboboxSource = new ObservableCollection<string>();

                foreach(ProductGroup pg in productController.GetAllProductGroups().ToList())
                {
                    ComboboxSource.Add(pg.Name);
                }

            }
            else if (DepartmentBool)
            {
                ComboboxSource = new ObservableCollection<string> {"Drift", "Utv/Förv"};
                System.Diagnostics.Debug.WriteLine("avdelning");
            }
            else if (OfficeBool)
            {

            }
        }

        private void SetResult()
        {
            if (OfficeBool)
            {
                Expense = budgetResultController.GetTotalBudget();
                Revenue = budgetResultController.GetRevenueBudgetByOffice();
            }
            else if (DepartmentBool)
            {
                Expense = budgetResultController.GetTotalBudgetByDepartment(ComboboxSelected);
                Revenue = budgetResultController.GetRevenueBudgetByDepartment(ComboboxSelected);
            }
            else if (ProductBool)
            {
                Expense = budgetResultController.GetTotalBudgetByProduct(ComboboxSelected);
                string customID = productController.GetByProductName(ComboboxSelected).CustomId;
                Revenue = budgetResultController.GetRevenueBudgetByProduct(customID);
            }
            else if (ProductGroupBool)
            {
                Expense = budgetResultController.GetTotalBudgetByProductGroup(ComboboxSelected);
                Revenue = budgetResultController.GetRevenueBudgetByProductGroup(ComboboxSelected);
            }

            Result = Revenue - Expense;
        }
    }
}
