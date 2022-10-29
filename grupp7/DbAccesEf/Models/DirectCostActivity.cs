using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf.Models;

namespace DbAccesEf.Models
{
    public class DirectCostActivity
    {
        public int DirectCostActivityID { get; set; }
        public double Cost { get; set; }
        public Activity Activity { get; set; }
    }
}
