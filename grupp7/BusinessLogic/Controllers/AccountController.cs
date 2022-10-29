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

        public List<Account> GetAllIncludeRelations()
        {
            return unitOfWork.AccountRepository.ReturnAllAccount();
        }

        //Change specific directCostProduct on specific account
        public void EditDirectCostProduct(double newDirectCost, int accountID, int productID)
        {
            Account account = GetAllIncludeRelations().ToList().FirstOrDefault(a => a.AccountId == accountID);
            DirectCostProduct directCostProduct = account.DirectCostProducts.FirstOrDefault(dp => dp.Product.ProductID == productID);
            directCostProduct.Cost = newDirectCost;
            unitOfWork.SaveChanges();
        }

        public void EditDirectCostActivity(double newDirectCost, int accountID, int activityID)
        {
            Account account = GetAllIncludeRelations().ToList().FirstOrDefault(a => a.AccountId == accountID);
            DirectCostActivity directCostActivity = account.DirectCostActivities.FirstOrDefault(da => da.Activity.ActivityID == activityID);
            directCostActivity.Cost = newDirectCost;
            unitOfWork.SaveChanges();
        }

        //Generate acitivty direct cost relations with zeroes on all accounts when adding new acitivty
        public void GenerateActivityDirectCostZeroes(Activity activity)
        {
            List<Account> accounts = GetAllIncludeRelations().ToList();

            foreach(Account a in accounts)
            {
                a.DirectCostActivities.Add(new DirectCostActivity()
                {
                    Cost = 0,
                    Activity = activity
                });
            }
            unitOfWork.SaveChanges();
        }

        //Generate product direct cost relations with zeroes on all accounts when adding new product
        public void GenerateProductDirectCostZeroes(Product product)
        {
            List<Account> accounts = GetAllIncludeRelations().ToList();

            foreach (Account a in accounts)
            {
                a.DirectCostProducts.Add(new DirectCostProduct()
                {
                    Cost = 0,
                    Product = product
                });
            }
            unitOfWork.SaveChanges();
        }
    }
}
