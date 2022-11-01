using DbAccesEf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf.Models;

namespace BusinessLogic.Controllers
{
    public class BudgetLockController
    {
        private UnitOfWork unitOfWork;

        public BudgetLockController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public bool GetRevenueBudgetLocked()
        {
            return unitOfWork.BudgetLockRepository.ReturnAll().ElementAt(0).RevenuBudgetLocked;
        }

        public void SetRevenueBudgetLock(bool setLock)
        {
            BudgetLock budgetLock = unitOfWork.BudgetLockRepository.FirstOrDefault(b => true);
            budgetLock.RevenuBudgetLocked = setLock;
            unitOfWork.SaveChanges();
        }
    }
}
