using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccesEf.Models
{
    //Personell contains a list of this, to store indivudal allocation on each product
    public class ProductAllocation
    {
        public int ProductAllocationID { get; set; }
        public int PersonellID { get; set; }
        public double Allocation { get; set; }
        public Product Product { get; set; }
    }
}
