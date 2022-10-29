using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccesEf.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public double SchablonExpense { get; set; }
        public List<DirectCostActivity> DirectCostActivities { get; set; }
        public List<DirectCostProduct> DirectCostProducts { get; set; }

        public Account()
        {
            DirectCostActivities = new List<DirectCostActivity>();
            DirectCostProducts = new List<DirectCostProduct>();
        }
    }
}
