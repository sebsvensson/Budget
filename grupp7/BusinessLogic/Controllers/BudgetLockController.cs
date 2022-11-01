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

        public BudgetLock GetBudgetLock()
        {
            return unitOfWork.BudgetLockRepository.FirstOrDefault(b => true);
        }

        public void SetRevenueBudgetLock(bool setLock)
        {
            BudgetLock budgetLock = unitOfWork.BudgetLockRepository.FirstOrDefault(b => true);
            budgetLock.RevenuBudgetLocked = setLock;
            unitOfWork.SaveChanges();
        }

        public void SetDirectCostBudgetLock(bool setLock, string department)
        {
            BudgetLock budgetLock = unitOfWork.BudgetLockRepository.FirstOrDefault(b => true);

            switch (department)
            {
                case "CD":
                    budgetLock.DirectCostDriftLocked = setLock;
                    break;
                case "CUOF":
                    budgetLock.DirectCostUtvLocked = setLock;
                    break;
                case "CA":
                    budgetLock.DirectCostAdmLocked = setLock;
                    break;
                case "CFOM":
                    budgetLock.DirectCostForsLocked = setLock;
                    break;
            }

            unitOfWork.SaveChanges();
        }
    }
}
