using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresentationLayer.Commands;
using PresentationLayer.ViewModels;

namespace PresentationLayer.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        private DataTable _testTable;
        public DataTable TestTable
        {
            get { return _testTable; }
            set
            {
                _testTable = value;
                OnPropertyChanged(null);
            }
        }

        private string _columnName;
        public string ColumnName
        {
            get { return _columnName; }
            set
            {
                _columnName = value;
                OnPropertyChanged(null);
            }
        }
        public TestViewModel()
        {
            //fyll data
            List<Product> products = new List<Product>();
            products.Add(new Product() { Number = 300});
            products.Add(new Product() { Number = 234 });
            products.Add(new Product() { Number = 534 });
            products.Add(new Product() { Number = 645 });
            products.Add(new Product() { Number = 123 });

            List<Konto> konton = new List<Konto>();
            konton.Add(new Konto() { Text = "egrerg", Products = products });
            konton.Add(new Konto() { Text = "hhtrtrh", Products = products });
            konton.Add(new Konto() { Text = "2342ddew", Products = products });
            konton.Add(new Konto() { Text = "hqduq", Products = products });

            this.TestTable = new DataTable();

            //Columns
            DataColumn column1 = new DataColumn();
            column1.ColumnName = "1";
            TestTable.Columns.Add(column1);
            DataColumn column2 = new DataColumn();
            column2.ColumnName = "2";
            TestTable.Columns.Add(column2);

            //rows
            DataRow row1 = TestTable.NewRow();
            row1[column1] = "hej";
            row1[column2] = "eeeh";
            TestTable.Rows.Add(row1);

            DataRow row2 = TestTable.NewRow();
            row2[column1] = "hhhh";
            row2[column2] = "uuuh";
            TestTable.Rows.Add(row2);
        }

        public void AddColumn(string name)
        {
            DataColumn newColumn = new DataColumn();
            newColumn.ColumnName = name;
            TestTable.Columns.Add(newColumn);
        }

        private ICommand _addColumnCommand;
        public ICommand AddColumnCommand
        {
            get
            {
                return _addColumnCommand ?? (_addColumnCommand = new CommandHandler(() => AddColumn(ColumnName)));
            }
        }
    }

    public class Konto
    {
        public string Text { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int Number { get; set; }
    }
}
