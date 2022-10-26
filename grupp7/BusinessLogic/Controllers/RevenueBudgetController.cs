using DbAccesEf;
using DbAccesEf.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
        public void AddRevenueBudget(Customer customer, Product product, int agreement,
            string gradeA, int additions, string gradeT, int budget, int hours, string comment)
        {
            
            RevenueBudget revenueBudget = new RevenueBudget();
            unitOfWork.RevenueBudgetRepository.Add(revenueBudget);
            {
                revenueBudget.Customer = customer;
                revenueBudget.Product = product;
                revenueBudget.Agreement = agreement;
                revenueBudget.Grade_A = gradeA;
                revenueBudget.Additions = additions;
                revenueBudget.Grade_T = gradeT;
                revenueBudget.Budget = budget;
                revenueBudget.Hours = hours;
                revenueBudget.Comment = comment;
            }
            unitOfWork.SaveChanges();
        }

        public IEnumerable<RevenueBudget> GetCustomerBudgets(string customer)
        {
            return unitOfWork.RevenueBudgetRepository.ReturnCustomerBudgets(customer);
        }

        public void RemoveRevenueBudget(RevenueBudget revenueBudget)
        {
            unitOfWork.RevenueBudgetRepository.Remove(revenueBudget);
        }

        public RevenueBudget GetRevenueBudget(int id)
        {
            return unitOfWork.RevenueBudgetRepository.FirstOrDefault(r => r.RevenueBudgetID == id);
        }
    }
}
