using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf.Models;
using BusinessLogic.Controllers;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Input;
using PresentationLayer.Commands;

namespace PresentationLayer.ViewModels
{
    public class ResourceAllocation2ViewModel : BaseViewModel
    {
        private PersonellController personellController;
        private List<Personell> personells;

        private DataTable tableCopy;
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

        //Save changes command
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new CommandHandler(() => Save()));
            }
        }

        public ResourceAllocation2ViewModel()
        {
            personellController = new PersonellController(new DbAccesEf.MyContext());
            personells = personellController.GetAll().ToList();

            //sort productallocations for each personell on product.productname
            foreach(Personell p in personells)
            {
                p.ProductAllocations = p.ProductAllocations.OrderBy(pa => pa.Product.ProductName).ToList();
            }

            Table = new DataTable();

            GenerateDataTable();

            tableCopy = Table.Copy();
        }

        public void Save()
        {

            //Go through every allocation cell in DataTable and save to database with correct personell and product
            
            //Send cell value, personellID and ProductID to controller

            //Iterate through each personell
            for(int i = 0; i < Table.Rows.Count; i++)
            {
                //Send new values to controller for each product, products starts on column 6
                for(int j = 6; j < Table.Columns.Count; j++)
                {
                    //Check if values has changed against tableCopy
                    if (Table.Rows[i].Field<double>(j) != tableCopy.Rows[i].Field<double>(j))
                    {
                        personellController.EditProductAllocation(Table.Rows[i].Field<double>(j),
                            personells.ElementAt(i).PersonellID,
                            personells.ElementAt(i).ProductAllocations.ElementAt(j - 6).Product.ProductID);
                    }
                }
            }
        }

        private void GenerateDataTable()
        {
            Table.Columns.Add("Namn", typeof(string));
            Table.Columns.Add("Sysselsättning", typeof(double));
            Table.Columns.Add("Vakansavdrag", typeof(double));
            Table.Columns.Add("Årsarbetare", typeof(double));
            Table.Columns.Add("Diff", typeof(double));
            Table.Columns.Add("Fördelat på produkter", typeof(double));

            //Product column headers
            foreach(ProductAllocation pa in personells.ElementAt(0).ProductAllocations)
            {
                Table.Columns.Add(RemoveSpecialChars(pa.Product.ProductName), typeof(double));
            }

            //Add rows
            for(int i = 0; i < personells.Count; i++)
            {
                Table.Rows.Add(BuildRow(i));
            }
        }

        private DataRow BuildRow(int index)
        {
            DataRow result = Table.NewRow();

            //Add non allocations stuff from personell (left-side of table)
            result[0] = personells.ElementAt(index).Name;
            result[1] = personells.ElementAt(index).EmploymentRate;
            result[2] = personells.ElementAt(index).VacancyDeduction;
            result[3] = personells.ElementAt(index).AnnualWorkRate;
            result[4] = 0;
            result[5] = 0;

            //Add product allocations on the remaining columns
            for(int i = 0; i < Table.Columns.Count - 6; i++)
            {
                result[i + 6] = personells.ElementAt(index).ProductAllocations.ElementAt(i).Allocation;
            }

            return result;
        }

        //Wpf datagrid empty cells bug with special char as header
        public string RemoveSpecialChars(string input)
        {
            return Regex.Replace(input, @"[^0-9a-zA-Z]", " ");
        }
    }
}
