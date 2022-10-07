using System;
using System.Collections.Generic;
using System.Text;

namespace DbAccessEf.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductGroup { get; set; }
    }
}
