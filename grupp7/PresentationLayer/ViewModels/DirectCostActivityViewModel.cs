using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Controllers;
using DbAccesEf.Models;
using DbAccesEf;
using System.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PresentationLayer.Commands;
using System.Text.RegularExpressions;

namespace PresentationLayer.ViewModels
{
    public class DirectCostActivityViewModel : BaseViewModel
    {
        private AccountController accountController;
        private ActivityController activityController;
        private List<Account> accounts;

        //Used to set columns of datagrid dynamically
        private List<Activity> activityColumns;

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
                SetActivityCombobox();
            }
        }

        private ObservableCollection<Activity> _activitiesComboBox;
        public ObservableCollection<Activity> ActivitiesComboBox
        {
            get { return _activitiesComboBox; }
            set
            {
                _activitiesComboBox = value;
                OnPropertyChanged(null);
            }
        }

        private Activity _selectedActivity;
        public Activity SelectedActivity
        {
            get { return _selectedActivity; }
            set
            {
                _selectedActivity = value;
                OnPropertyChanged(null);
            }
        }

        private ICommand _addActivityCommand;
        public ICommand AddActivityCommand
        {
            get
            {
                return _addActivityCommand ?? (_addActivityCommand = new CommandHandler(() => AddActivity()));
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

        public DirectCostActivityViewModel()
        {

            MyContext context = new MyContext();
            accountController = new AccountController(context);
            activityController = new ActivityController(context);

            activityColumns = new List<Activity>();
            accounts = accountController.GetAllIncludeRelations();

            foreach (Account a in accounts)
            {
                a.DirectCostActivities = a.DirectCostActivities.OrderBy(dp => dp.Activity.ActivityName).ToList();
            }

            Departments = new ObservableCollection<string>() { "FO", "AO" };

            //ProductsComboBox = new ObservableCollection<Product>(productController.GetAll().ToList());
            SetActivityCombobox();

            //productColumns.Add(productController.GetAll().ElementAt(0));
            //productColumns.Add(productController.GetAll().ElementAt(1));

            GenerateDataTable();
        }

        private void SetActivityCombobox()
        {
            if (SelectedDepartment != null)
            {
                ActivitiesComboBox = new ObservableCollection<Activity>(activityController.GetByDepartment(SelectedDepartment).ToList());
            }
        }

        private void GenerateDataTable()
        {
            //Save to database before generating new table

            Table = new DataTable();

            Table.Columns.Add("Konto", typeof(int));
            Table.Columns.Add("Benämning", typeof(string));

            //Sort product columns
            activityColumns = activityColumns.OrderBy(ac => ac.ActivityName).ToList();

            //Add columns
            foreach (Activity a in activityColumns)
            {
                Table.Columns.Add(RemoveSpecialChars(a.ActivityName), typeof(double));
            }

            //Add account rows 
            for (int i = 0; i < accounts.Count; i++)
            {
                DataRow r = Table.NewRow();
                r[0] = accounts.ElementAt(i).AccountNumber;
                r[1] = accounts.ElementAt(i).Name;

                //Add costs
                for (int j = 0; j < Table.Columns.Count - 2; j++)
                {
                    //Get value based on productColumns
                    r[j + 2] = accounts.ElementAt(i).DirectCostActivities.FirstOrDefault(da => da.Activity.ActivityID == activityColumns.ElementAt(j).ActivityID).Cost;
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
            TableUserControl = new DirectCostActivityDataGridViewModel(Table);
        }

        private void AddActivity()
        {
            //Add new product column if it doesnt already exist
            if (!activityColumns.Any(a => a == SelectedActivity))
            {
                activityColumns.Add(SelectedActivity);

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
                for (int j = 2; j < Table.Columns.Count; j++)
                {
                    //Check if cell is changed
                    if (oldTable.Rows[i + 1][j] != null && Table.Rows[i + 1].Field<double>(j) != oldTable.Rows[i + 1].Field<double>(j))
                    {
                        //Get productID from productColumns, because its sorted based on chosen products
                        accountController.EditDirectCostActivity(Table.Rows[i + 1].Field<double>(j), accounts.ElementAt(i).AccountId, activityColumns.ElementAt(j - 2).ActivityID);
                    }
                }
            }

            //Update accounts after database is updated
            accounts = accountController.GetAllIncludeRelations();

            foreach (Account a in accounts)
            {
                a.DirectCostActivities = a.DirectCostActivities.OrderBy(da => da.Activity.ActivityName).ToList();
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
    }
}
