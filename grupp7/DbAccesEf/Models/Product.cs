using System;
using System.Collections.Generic;
using System.Text;
using DbAccesEf.Models;

namespace DbAccesEf.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string CustomId { get; set; }
        public string Xxxx { get; set; }
        public string ProductName { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductGroup ProductGroup { get; set; }
    }
}
