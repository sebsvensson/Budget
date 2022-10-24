using DbAccesEf;
using DbAccesEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Controllers
{
    public class RevenueBudgetController
    {
        private UnitOfWork unitOfWork;

        public RevenueBudgetController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<RevenueBudget> GetAllRevenueBudgets()
        {
            return unitOfWork.RevenueBudgetRepository.ReturnAll();
        }

        public RevenueBudget GetByCustomerID(string ID)
        {
            return unitOfWork.RevenueBudgetRepository.FirstOrDefault(c => c.Customer.CustomID == ID);
        }
    }
}
