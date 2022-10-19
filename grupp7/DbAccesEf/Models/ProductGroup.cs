using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccesEf.Models
{
    public class ProductGroup
    {
        public ProductGroup(string name)
        {
            Name = name;
        }
        public int ProductGroupID { get; set; }
        public string Name { get; set; }
    }
}
