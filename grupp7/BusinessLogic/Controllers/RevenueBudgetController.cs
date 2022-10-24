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
        public void AddRevenueBudget(string customerID, string customerName, string productID, string productName, int agreement,
            string gradeA, int additions, string gradeT, int budget, int hours, string comment)
        {

            RevenueBudget revenueBudget = new RevenueBudget();
            unitOfWork.RevenueBudgetRepository.Add(revenueBudget);
            {
                
                revenueBudget.Customer.CustomID = customerID;
                revenueBudget.Customer.CustomerName = customerName;
                revenueBudget.Product.CustomId = productID;
                revenueBudget.Product.ProductName = productName;
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

        
    }
}
