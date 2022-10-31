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

namespace PresentationLayer.ViewModels
{
    public class ForeCastingViewModel : BaseViewModel
    {
        private ResourceController resourceController;
        private ProductController productController;
        private RevenueBudgetController revenueBudgetController;
        private BudgetResultController budgetResultController;
        private List<string> fileRows;

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

        public ForeCastingViewModel()
        {
            MyContext context = new MyContext();
            resourceController = new ResourceController(context);
            productController = new ProductController(context);
            revenueBudgetController = new RevenueBudgetController(context);
            budgetResultController = new BudgetResultController(context);

            Table = new DataTable();
            fileRows = resourceController.ReadRevenueProductCustomerRow();

            System.Diagnostics.Debug.WriteLine(fileRows.ElementAt(1));
            GenerateTable();
            
        }

        private void GenerateTable()
        {
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
            foreach(string row in fileRows)
            {
                Table.Rows.Add(GenerateRow(row));
            }

        }

        private DataRow GenerateRow(string row)
        {
            DataRow result = Table.NewRow();
            System.Diagnostics.Debug.WriteLine(row);
            System.Diagnostics.Debug.WriteLine("-----");
            string[] cells = row.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            //Product column
            if(productController.GetByID(cells[0]) != null)
            {
                result[0] = productController.GetByID(cells[0]).ProductName;
            }
            else
            {
                result[0] = cells[0];
            }


            //Budget column
            if (productController.GetByID(cells[0]) == null)
            {
                result[1] = 0;
            }
            else
            {
                result[1] = budgetResultController.GetRevenueBudgetByProduct(productController.GetByID(cells[0]).CustomId);
            }

            return result;

        }
    }
}
