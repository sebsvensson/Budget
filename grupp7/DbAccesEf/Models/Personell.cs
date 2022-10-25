using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccesEf.Models
{
    public class Personell
    {
        public int PersonellID { get; set; }
        public string Pnr { get; set; }
        public string Name { get; set; }
        public double MonthlySalary { get; set; }
        public double EmploymentRate { get; set; }
        public double VacancyDeduction { get; set; }
        public double AnnualWorkRate { get; set; }
        public double Adm { get; set; }
        public double ForsMark { get; set; }
        public double UtvForv { get; set; }
        public double Drift { get; set; }      
        public List<ProductAllocation> ProductAllocations { get; set; }
        //public ProductAllocation SelectedAllocation { get; set; }
        //public double AllocationInput { get; set; }

        public Personell()
        {
            ProductAllocations = new List<ProductAllocation>();
        }
    }
}
