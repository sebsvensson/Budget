using DbAccesEf;
using DbAccesEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Controllers
{
    public class YieldController
    {
        private UnitOfWork unitOfWork;

        public YieldController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public Yield GetYield()
        {
            return unitOfWork.YieldRepository.ReturnAll().First();
        }

        public void UpdateYield(Yield yield)
        {
            unitOfWork.YieldRepository.Update(yield);
            unitOfWork.SaveChanges();
        }

        public void EditYield(double amount)
        {
            Yield yield = GetYield();
            yield.Amount = amount;
            UpdateYield(yield);
        }

    }
}
