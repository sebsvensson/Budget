using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccesEf.Models
{
    public class RevenueBudget
    {
        public int RevenueBudgetID { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public int Agreement { get; set; }
        public string Grade_A { get; set; }
        public string Grade_T { get; set; }
        public int Additions { get; set; }
        public int Budget { get; set; }
        public int Hours { get; set; }
        public string Comment { get; set; }
    }
}
