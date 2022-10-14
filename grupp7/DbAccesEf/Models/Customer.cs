using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccesEf.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomID { get; set; }
        public string CustomerName { get; set; }
        public CustomerCategory Category { get; set; }
    }
}
