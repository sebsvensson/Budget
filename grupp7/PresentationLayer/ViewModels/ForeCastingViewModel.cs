using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationLayer.ViewModels;
using BusinessLogic.Controllers;
using System.Data;
using DbAccesEf;
using DbAccesEf.Models;
using System.Globalization;
using System.Windows.Input;
using PresentationLayer.Commands;

namespace PresentationLayer.ViewModels
{
    public class ForeCastingViewModel : BaseViewModel
    {
        private ResourceController resourceController;
        private ProductController productController;
        private RevenueBudgetController revenueBudgetController;
        private BudgetResultController budgetResultController;
        private List<string> fileRows;
        private List<double> upparbetatOld;
        private List<double> prognosOld;

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

        private DataTable _table;
        public DataTable Table
        {
            get { return _table; }
            set
            {
                _table = value;
                OnPropertyChanged(null);
            }
        }

        private List<string> _monthNames;
        public List<string> MonthNames
        {
            get { return _monthNames; }
            set
            {
                _monthNames = value;
                OnPropertyChanged(null);
            }
        }

        private int _selectedMonth;
        public int SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value + 1;
                OnPropertyChanged(null);
                GenerateTable();
            }
        }

        private ICommand _updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new CommandHandler(() => Update()));
            }
        }

        public ForeCastingViewModel()
        {
            MyContext context = new MyContext();
            resourceController = new ResourceController(context);
            productController = new ProductController(context);
            revenueBudgetController = new RevenueBudgetController(context);
            budgetResultController = new BudgetResultController(context);

            Table = new DataTable();
            fileRows = resourceController.ReadRevenueProductCustomerRow();
            GenerateTable();

            MonthNames = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList();
            
        }

        private void GenerateTable()
        {
            //save old upparbetat
            upparbetatOld = new List<double>();
            prognosOld = new List<double>();
            for (int i = 0; i < Table.Rows.Count; i++)
            {
                upparbetatOld.Add(Table.Rows[i].Field<double>(4));
                prognosOld.Add(Table.Rows[i].Field<double>(7));
            }



            Table = new DataTable();
            //Add columns
            Table.Columns.Add("Produkt", typeof(string));
            Table.Columns.Add("Budget", typeof(double));
            Table.Columns.Add("Utfall mån", typeof(double));
            Table.Columns.Add("Utfall acc", typeof(double));
            Table.Columns.Add("Upparbetat", typeof(double));
            Table.Columns.Add("Trend", typeof(double));
            Table.Columns.Add("Föreg. prognos", typeof(double));
            Table.Columns.Add("Prognos", typeof(double));
            Table.Columns.Add("Prognos-Budget", typeof(double));


            //Add rows
            int counter = 0;
            foreach(string row in fileRows.Skip(1))
            {
                if(GenerateRow(row, counter) != null)
                {
                    Table.Rows.Add(GenerateRow(row, counter));
                    counter++;
                }
            }

            TableUserControl = new DirectCostDataGridViewModel(Table);
        }

        private DataRow GenerateRow(string row, int counter)
        {

            DataRow result = Table.NewRow();
            //string[] cells = row.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            string[] cells = row.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

            //Check if product already exists
            for (int i = 0; i < Table.Rows.Count; i++)
            {
                if(Table.Rows[i].Field<string>(0) == cells[1])
                {
                    return null;
                } 
            }


            //Product column
            /*if (productController.GetByID(cells[0]) != null)
            {
                result[0] = productController.GetByID(cells[0]).ProductName;
            }
            else
            {
                result[0] = cells[0];
            }*/

            //Product column
            result[0] = cells[1];

            //Budget column
            if (productController.GetByID(cells[0]) == null)
            {
                result[1] = 0;
            }
            else
            {
                result[1] = budgetResultController.GetRevenueBudgetByProduct(productController.GetByID(cells[0]).CustomId);
            }

            //Utfall mån
            if (DatestringToMonthNumber(cells[4]) == SelectedMonth)
            {
                double costMonth = 0;
                //costMonth += Convert.ToDouble(cells[5]) * -1;
                
                //Check for mathcing products
                foreach(string r in fileRows)
                {
                    string[] c = r.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (c[0] == cells[0] && DatestringToMonthNumber(c[4]) == SelectedMonth)
                    {
                        System.Diagnostics.Debug.WriteLine(c[5]);
                        costMonth += (Convert.ToDouble(c[5]) * -1);
                    }
                }

                result[2] = costMonth;
            }
            else
            {
                result[2] = 0;
            }

            //utfall acc
            double costAcc = 0;

            foreach (string r in fileRows)
            {
                string[] c = r.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (c[0] == cells[0])
                {
                    System.Diagnostics.Debug.WriteLine(c[5]);
                    costAcc += (Convert.ToDouble(c[5]) * -1);
                }
            }
            result[3] = costAcc;

            //Upparbetat
            if(upparbetatOld.Count > counter)
            {
                result[4] = upparbetatOld.ElementAt(counter);
            }
            else
            {
                result[4] = 0;
            }

            //trend
            double trendResult = 0;
            if (upparbetatOld.Count > counter)
            {
                trendResult = (costAcc + upparbetatOld.ElementAt(counter)) / SelectedMonth * 12;
            }

            result[5] = trendResult;

            //föreg prognos
            result[6] = 0;

            //prognos
            if (prognosOld.Count > counter)
            {
                result[7] = prognosOld.ElementAt(counter);
            }
            else
            {
                result[7] = 0;
            }

            //Prognos-budget
            if (prognosOld.Count > counter)
            {
                result[8] = result.Field<double>(1) - prognosOld.ElementAt(counter);
            }
            else
            {
                result[8] = 0;
            }

            return result;

        }

        private int DatestringToMonthNumber(string dateString)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "yyyyMMdd";
            DateTime result = DateTime.ParseExact(dateString, format, provider);

            return result.Month;

        }

        private void Update()
        {
            GenerateTable();
        }
    }
}
