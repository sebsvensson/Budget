using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccesEf.Models
{
    public class ProductCategory
    {
        public ProductCategory(string name)
        {
            Name = name;
        }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }

    }
}
