using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf;
using DbAccesEf.Models;

namespace BusinessLogic.Controllers
{
    public class AccountController
    {
        private UnitOfWork unitOfWork;

        public AccountController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void ChangeSchablonExpense(int accountID, double newSchablonExpense)
        {
            Account account = unitOfWork.AccountRepository.FirstOrDefault(a => a.AccountId == accountID);
            account.SchablonExpense = newSchablonExpense;
            unitOfWork.SaveChanges();
        }

        public IEnumerable<Account> GetAll()
        {
            return unitOfWork.AccountRepository.ReturnAll();
        }
    }
}
