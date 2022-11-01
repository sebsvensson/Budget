using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf.Models;
using BusinessLogic.Controllers;
using System.Collections.ObjectModel;
using DbAccesEf;
using System.Windows.Input;
using PresentationLayer.Commands;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace PresentationLayer.ViewModels
{
    public class DirectCostProductViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        private AccountController accountController;
        private ProductController productController;
        private BudgetLockController budgetLockController;
        private PersonellController personellController;
        private List<Account> accounts;

        //Used to set columns of datagrid dynamically
        private List<Product> productColumns;

        private DataTable oldTable;
        private DataTable _table;
        public DataTable Table
        {
            get { return _table; }
            set
            {
                _table = value;
                OnPropertyChanged("Table");                
            }
        }

        //User control to load table
        private BaseViewModel _tableUserControl;
        public BaseViewModel TableUserControl
        {
            get { return _tableUserControl; }
            set
            {
                _tableUserControl = value;
                OnPropertyChanged(null);
            }
        }

        private ObservableCollection<string> _departments;
        public ObservableCollection<string> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                OnPropertyChanged(null);
            }
        }

        private string _selectedDepartment;
        public string SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(null);
                SetProductCombobox();

                //Set budgetLocked based on selected department
                if (SelectedDepartment == "Utv/Förv" && budgetLockController.GetBudgetLock().DirectCostUtvLocked)
                {
                    BudgetNotLocked = false;
                }
                else if(SelectedDepartment == "Drift" && budgetLockController.GetBudgetLock().DirectCostDriftLocked)
                {
                    BudgetNotLocked = false;
                }
                else
                {
                    BudgetNotLocked = true;
                }

                //Set lock acces
                if(SelectedDepartment == "Utv/Förv" && mainViewModel.loggedInUser.PermissionLevel == "CUOF")
                {
                    LockAccess = true;
                }
                else if (SelectedDepartment == "Drift" && mainViewModel.loggedInUser.PermissionLevel == "CD")
                {
                    LockAccess = true;
                }
                else if(mainViewModel.loggedInUser.PermissionLevel == "CE")
                {
                    LockAccess = true;
                }
                else
                {
                    LockAccess = false;
                }

                //Reset datatable when changing department
                productColumns = new List<Product>();
                GenerateDataTable();
                SetLockText();
                NotAllocatedText = "";
            }
        }

        private ObservableCollection<Product> _productsComboBox;
        public ObservableCollection<Product> ProductsComboBox
        {
            get { return _productsComboBox; }
            set
            {
                _productsComboBox = value;
                OnPropertyChanged(null);
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(null);
            }
        }

        private ICommand _addProductCommand;
        public ICommand AddProductCommand
        {
            get
            {
                return _addProductCommand ?? (_addProductCommand = new CommandHandler(() => AddProduct()));
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new CommandHandler(() => Save()));
            }
        }

        private ICommand _lockCommand;
        public ICommand LockCommand
        {
            get
            {
                return _lockCommand ?? (_lockCommand = new CommandHandler(() => LockBudget()));
            }
        }

        private bool _budgetNotLocked;
        public bool BudgetNotLocked
        {
            get { return _budgetNotLocked; }
            set
            {
                _budgetNotLocked = value;
                OnPropertyChanged(null);
            }
        }

        private bool _lockAccess;
        public bool LockAccess
        {
            get { return _lockAccess; }
            set
            {
                _lockAccess = value;
                OnPropertyChanged(null);
            }
        }

        private string _lockText;
        public string LockText
        {
            get { return _lockText; }
            set
            {
                _lockText = value;
                OnPropertyChanged(null);
            }
        }

        private string _notAllocatedText;
        public string NotAllocatedText
        {
            get { return _notAllocatedText; }
            set
            {
                _notAllocatedText = value;
                OnPropertyChanged(null);
            }
        }

        public DirectCostProductViewModel(MainViewModel mainViewModel)
        {
            LockAccess = false;
            BudgetNotLocked = true;

            this.mainViewModel = mainViewModel;
            MyContext context = new MyContext();
            accountController = new AccountController(context);
            productController = new ProductController(context);
            budgetLockController = new BudgetLockController(context);
            personellController = new PersonellController(context);

            productColumns = new List<Product>();
            accounts = accountController.GetAllIncludeRelations();
            System.Diagnostics.Debug.WriteLine(accounts.Count);

            foreach (Account a in accounts)
            {
                a.DirectCostProducts = a.DirectCostProducts.OrderBy(dp => dp.Product.ProductName).ToList();
            }

            Departments = new ObservableCollection<string>() { "Utv/Förv", "Drift" };

            //ProductsComboBox = new ObservableCollection<Product>(productController.GetAll().ToList());
            SetProductCombobox();

            //productColumns.Add(productController.GetAll().ElementAt(0));
            //productColumns.Add(productController.GetAll().ElementAt(1));

            GenerateDataTable();
            SetLockText();
        }

        private void SetProductCombobox()
        {
            if(SelectedDepartment != null)
            {
                ProductsComboBox = new ObservableCollection<Product>(productController.GetByDepartment(SelectedDepartment).ToList());
            }
        }

        private void GenerateDataTable()
        {
            //Save to database before generating new table

            Table = new DataTable();

            Table.Columns.Add("Konto", typeof(int));
            Table.Columns.Add("Benämning", typeof(string));

            //Sort product columns
            productColumns = productColumns.OrderBy(pc => pc.ProductName).ToList();
            
            //Add columns
            foreach (Product p in productColumns)
            {
                Table.Columns.Add(RemoveSpecialChars(p.ProductName), typeof(double));
            }

            //Add account rows 
            for(int i = 0; i < accounts.Count; i++)
            {
                DataRow r = Table.NewRow();
                r[0] = accounts.ElementAt(i).AccountNumber;
                r[1] = accounts.ElementAt(i).Name;

                //Add costs
                for(int j = 0; j < Table.Columns.Count - 2; j++)
                {
                    //Get value based on productColumns
                    //System.Diagnostics.Debug.WriteLine(accounts.ElementAt(i).DirectCostProducts.Count);
                    r[j + 2] = accounts.ElementAt(i).DirectCostProducts.FirstOrDefault(dp => dp.Product.ProductID == productColumns.ElementAt(j).ProductID).Cost;
                }

                Table.Rows.Add(r);
            }

            //Add sum row
            DataRow sumRow = Table.NewRow();
            sumRow[1] = "Summa : ";
            for (int i = 0; i < Table.Columns.Count - 2; i++)
            {
                double sum = 0;
                for (int j = 0; j < accounts.Count; j++)
                {
                    sum += Table.Rows[j].Field<double>(i + 2);
                }
                sumRow[i + 2] = sum;
            }

            Table.Rows.InsertAt(sumRow, 0);

            oldTable = Table.Copy();
            TableUserControl = new DirectCostDataGridViewModel(Table);
        }

        private void AddProduct()
        {          
            //Add new product column if it doesnt already exist
            if(!productColumns.Any(p => p == SelectedProduct))
            {
                productColumns.Add(SelectedProduct);

                //save to database before updating table
                SaveToDataBase();

                //Update DataTable
                GenerateDataTable();
            }
        }

        private void SaveToDataBase()
        {
            //Go throuch each cell in Table and save to correct account and product

            //Iterate through each account
            for (int i = 0; i < Table.Rows.Count - 1; i++)
            {
                //Products starts at column 2
                for(int j = 2; j < Table.Columns.Count; j++)
                {
                    //Check if cell is changed
                    if (oldTable.Rows[i + 1][j] != null && Table.Rows[i + 1].Field<double>(j) != oldTable.Rows[i + 1].Field<double>(j))
                    {
                        //Get productID from productColumns, because its sorted based on chosen products
                        accountController.EditDirectCostProduct(Table.Rows[i + 1].Field<double>(j), accounts.ElementAt(i).AccountId, productColumns.ElementAt(j - 2).ProductID);
                    }
                }
            }

            //Update accounts after database is updated
            accounts = accountController.GetAllIncludeRelations();

            foreach (Account a in accounts)
            {
                a.DirectCostProducts = a.DirectCostProducts.OrderBy(dp => dp.Product.ProductName).ToList();
            }
        }

        private void Save()
        {
            SaveToDataBase();
            GenerateDataTable();
        }

        public string RemoveSpecialChars(string input)
        {
            return Regex.Replace(input, @"[^0-9a-zA-Z]", " ");
        }

        private void LockBudget()
        {
            //Check if personell is fully allocated on selected department
            if (!personellController.IsPersonellAllocatedOnDepartment(SelectedDepartment))
            {
                NotAllocatedText = "Personall ej fullt allokerad";
                return;
            }

            NotAllocatedText = "";

            //Check if logged in user and chosen department match
            if(SelectedDepartment == "Utv/Förv" && mainViewModel.loggedInUser.PermissionLevel == "CUOF")
            {
                budgetLockController.SetDirectCostBudgetLock(!budgetLockController.GetBudgetLock().DirectCostUtvLocked, mainViewModel.loggedInUser.PermissionLevel);
            }

            else if (SelectedDepartment == "Drift" && mainViewModel.loggedInUser.PermissionLevel == "CD")
            {
                budgetLockController.SetDirectCostBudgetLock(!budgetLockController.GetBudgetLock().DirectCostDriftLocked, mainViewModel.loggedInUser.PermissionLevel);
            }

            //Lock when CE is logged in
            else if(mainViewModel.loggedInUser.PermissionLevel == "CE")
            {
                if (SelectedDepartment == "Drift")
                {
                    budgetLockController.SetDirectCostBudgetLock(!budgetLockController.GetBudgetLock().DirectCostDriftLocked, "CD");
                }
                else if(SelectedDepartment == "Utv/Förv")
                {
                    budgetLockController.SetDirectCostBudgetLock(!budgetLockController.GetBudgetLock().DirectCostUtvLocked, "CUOF");
                }
            }

            SetNotLocked();
            SetLockText();
        }

        //Sets datagrid locked or not based on chosen department and database
        private void SetNotLocked()
        {
            if(SelectedDepartment == "Drift")
            {
                BudgetNotLocked = !budgetLockController.GetBudgetLock().DirectCostDriftLocked;
            }
            else if(SelectedDepartment == "Utv/Förv")
            {
                BudgetNotLocked = !budgetLockController.GetBudgetLock().DirectCostUtvLocked;
            }
        }

        private void SetLockText()
        {
            if (BudgetNotLocked)
            {
                LockText = "Lås";
            }
            else
            {
                LockText = "Lås upp";
            }
        }
    }
}
