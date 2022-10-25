using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf.Models;
using BusinessLogic.Controllers;
using System.Data;

namespace PresentationLayer.ViewModels
{
    public class ResourceAllocation2ViewModel : BaseViewModel
    {
        private PersonellController personellController;
        private List<Personell> personells;

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

        public ResourceAllocation2ViewModel()
        {
            personellController = new PersonellController(new DbAccesEf.MyContext());
            personells = personellController.GetAll().ToList();
            Table = new DataTable();

            GenerateDataTable();

        }

        private void GenerateDataTable()
        {
            Table.Columns.Add("Namn");
            Table.Columns.Add("Sysselsättning");
            Table.Columns.Add("Vakansavdrag");
            Table.Columns.Add("Årsarbetare");
            Table.Columns.Add("Diff");
            Table.Columns.Add("Fördelat på produkter");

            foreach(ProductAllocation pa in personells.ElementAt(0).ProductAllocations)
            {
                Table.Columns.Add(pa.Product.ProductName, typeof(double));
            }

            //Add rows
            Table.Rows.Add(personells.ElementAt(0).Name,
                personells.ElementAt(0).EmploymentRate,
                personells.ElementAt(0).VacancyDeduction,
                personells.ElementAt(0).AnnualWorkRate,
                null,
                null,
                GetAllocationArray(0));
        }

        private double[] GetAllocationArray(int index)
        {
            //create new array of allocations on specifix personell
            double[] result = new double[personells.ElementAt(index).ProductAllocations.Count];
            for(int i = 0; i < result.Length; i++)
            {
                // add allocation value of each product on specific personell
                result[i] = personells.ElementAt(index).ProductAllocations.ElementAt(i).Allocation;
            }

            return result;
        }
    }
}
