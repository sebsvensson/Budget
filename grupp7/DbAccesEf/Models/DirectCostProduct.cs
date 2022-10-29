using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccesEf.Models
{
    public class DirectCostProduct
    {
        public int DirectCostProductID { get; set; }
        public double Cost { get; set; }
        public Product Product { get; set; }
    }
}
