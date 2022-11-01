using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccesEf.Models
{
    public class BudgetLock
    {
        public int BudgetLockID { get; set; }
        public bool RevenuBudgetLocked { get; set; }
        public bool DirectCostDriftLocked { get; set; }
        public bool DirectCostUtvLocked { get; set; }
        public bool DirectCostAdmLocked { get; set; }
        public bool DirectCostForsLocked { get; set; }
    }
}
