using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf;
using DbAccesEf.Models;

namespace BusinessLogic.Controllers
{
    public class BudgetResultController
    {
        private UnitOfWork unitOfWork;
        private double addonPercentage = 0;

        public BudgetResultController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
            GetTotalBudget();
        }

        public double GetTotalBudget()
        {
            double totalSalary = 0;
            double totalEmployedRate = 0;
            double totalEmployedRateDriftUtv = 0;

            foreach(Personell p in unitOfWork.PersonellRepository.ReturnAll())
            {
                totalEmployedRate += p.AnnualWorkRate;
                if(p.UtvForv != 0 || p.Drift != 0)
                {
                    totalSalary += (p.Drift + p.UtvForv) * p.MonthlySalary;
                    totalEmployedRateDriftUtv += p.AnnualWorkRate;
                }
            }

            double totalProductCost = 0;
            foreach(DirectCostProduct dp in unitOfWork.DirectCostProductRepository.ReturnAll().ToList())
            {
                totalProductCost += dp.Cost;
            }

            double totalSchablon = 0;
            //5023 - 5522
            foreach(Account a in unitOfWork.AccountRepository.ReturnAll())
            {
                if (a.AccountNumber >= 5023 && a.AccountNumber <= 5522)
                {
                    totalSchablon += a.SchablonExpense;
                }
            }

            double snittSchablon = totalSchablon / totalEmployedRate * totalEmployedRateDriftUtv;
            double totalTillverksningsKostnad = totalSalary + snittSchablon + totalProductCost;

            addonPercentage = GetAddon() / totalTillverksningsKostnad + 1;

            //Cost for whole office
            return Math.Round(addonPercentage * totalTillverksningsKostnad, 1);
        }

        public double GetTotalBudgetByProduct(string productName)
        {
            Product selectedProduct = unitOfWork.ProductRepository.FirstOrDefault(p => p.ProductName == productName);

            double totalEmployedRate = 0;
            double totalEmployedRateProduct = 0;
            double totalEmployedRateDepartment = 0;
            double totalSalaryDepartment = 0;
            double totalSchablon = 0;
            double totalCostProduct = 0;

            foreach (Personell p in unitOfWork.PersonellRepository.ReturnAllPersonell())
            {
                totalEmployedRate += p.AnnualWorkRate;
                List<ProductAllocation> productAllocations = p.ProductAllocations.Where(pa => pa.Product == selectedProduct).ToList();

                foreach(ProductAllocation pa in productAllocations)
                {
                    totalEmployedRateProduct += pa.Allocation;
                }
            }

            string department = selectedProduct.Department;


            foreach (Personell p in unitOfWork.PersonellRepository.ReturnAll())
            {
                if (department == "Drift" && p.Drift != 0)
                {
                    totalEmployedRateDepartment += p.Drift;
                    totalSalaryDepartment += p.MonthlySalary * p.Drift;
                }
                else if(department == "Utv/Förv" && p.UtvForv != 0)
                {
                    totalEmployedRateDepartment += p.UtvForv;
                    totalSalaryDepartment += p.MonthlySalary * p.UtvForv;
                }
            }

            foreach (Account a in unitOfWork.AccountRepository.ReturnAll())
            {
                if (a.AccountNumber >= 5023 && a.AccountNumber <= 5522)
                {
                    totalSchablon += a.SchablonExpense;
                }
            }

            //Get all costs from product
            foreach(Account a in unitOfWork.AccountRepository.ReturnAllAccount())
            {
                totalCostProduct += a.DirectCostProducts.FirstOrDefault(dp => dp.Product == selectedProduct).Cost;
            }

            double snittSchablon = totalSchablon / totalEmployedRate * totalEmployedRateDepartment;
            double totalCostProductResult = (totalEmployedRateProduct / totalEmployedRateDepartment) * (totalSalaryDepartment + snittSchablon) + totalCostProduct;

            return Math.Round(totalCostProductResult * addonPercentage, 1);
        }

        public double GetTotalBudgetByProductGroup(string productGroup)
        {

            ProductGroup selectedProductGroup = unitOfWork.ProductGroupRepository.FirstOrDefault(pg => pg.Name == productGroup);
            List<Product> selectedProducts = unitOfWork.ProductRepository.Find(p => p.ProductGroup.Name == selectedProductGroup.Name).ToList();

            double result = 0;

            foreach(Product p in selectedProducts)
            {
                result += GetTotalBudgetByProduct(p.ProductName);
            }

            return result;
        }

        public double GetTotalBudgetByDepartment(string department)
        {
            double totalSalary = 0;
            double totalEmployedRate = 0;
            double totalEmployedRateDriftUtv = 0;

            foreach (Personell p in unitOfWork.PersonellRepository.ReturnAll())
            {
                totalEmployedRate += p.AnnualWorkRate;

                if (department == "Utv/Förv")
                {
                    if (p.UtvForv != 0)
                    {
                        totalSalary += p.UtvForv * p.MonthlySalary;
                        totalEmployedRateDriftUtv += p.AnnualWorkRate;
                    }
                }


                else if (department == "Drift")
                {
                    if (p.Drift != 0)
                    {
                        totalSalary += p.Drift * p.MonthlySalary;
                        totalEmployedRateDriftUtv += p.AnnualWorkRate;
                    }
                }
            }

            double totalProductCost = 0;
            foreach (DirectCostProduct dp in unitOfWork.DirectCostProductRepository.ReturnAllDirectCostProduct().ToList())
            {
                if(dp.Product.Department == department)
                {
                    totalProductCost += dp.Cost;
                }
            }

            double totalSchablon = 0;
            //5023 - 5522
            foreach (Account a in unitOfWork.AccountRepository.ReturnAll())
            {
                if (a.AccountNumber >= 5023 && a.AccountNumber <= 5522)
                {
                    totalSchablon += a.SchablonExpense;
                }
            }

            double snittSchablon = totalSchablon / totalEmployedRate * totalEmployedRateDriftUtv;
            double totalTillverksningsKostnad = totalSalary + snittSchablon + totalProductCost;

            //Cost for whole department
            return Math.Round(addonPercentage * totalTillverksningsKostnad, 1);
        }

        private double GetAddon()
        {
            double totalSalary = 0;
            double activityCost = 0;

            foreach (Personell p in unitOfWork.PersonellRepository.ReturnAll())
            {
                if (p.Adm != 0 || p.ForsMark != 0)
                {
                    totalSalary += p.MonthlySalary  * (p.Adm + p.ForsMark);
                }
            }

            foreach(Account a in unitOfWork.AccountRepository.ReturnAllAccount())
            {
                foreach(DirectCostActivity da in a.DirectCostActivities)
                {
                    activityCost += da.Cost;
                }
            }

            return totalSalary + activityCost + unitOfWork.YieldRepository.FirstOrDefault(y => true).Amount;

        }

        public double GetRevenueBudgetByOffice()
        {
            double totalRevenueBudget = 0;

            foreach(RevenueBudget rb in unitOfWork.RevenueBudgetRepository.ReturnAll())
            {
                totalRevenueBudget += rb.Budget;
            }

            return totalRevenueBudget;
        }

        public double GetRevenueBudgetByProduct(string customID)
        {
            List<RevenueBudget> revenueBudgets = unitOfWork.RevenueBudgetRepository.ReturnProductBudgets(customID).ToList();
            double totalRevenueBudget = 0;
            foreach(RevenueBudget rb in revenueBudgets)
            {
                totalRevenueBudget += rb.Budget;
            }

            return totalRevenueBudget;
        }

        public double GetRevenueBudgetByProductGroup(string productGroup)
        {
            List <RevenueBudget> revenueBudgets = unitOfWork.RevenueBudgetRepository.ReturnAllRevenueBudgets().ToList();
            double totalRevenueBudget = 0;

            foreach(RevenueBudget rb in revenueBudgets)
            {
                if(rb.Product.ProductGroup.Name == productGroup)
                {
                    totalRevenueBudget += rb.Budget;
                }
            }

            return totalRevenueBudget;
        }

        public double GetRevenueBudgetByDepartment(string department)
        {
            List<RevenueBudget> revenueBudgets = unitOfWork.RevenueBudgetRepository.ReturnAllRevenueBudgets().ToList();
            double totalRevenueBudget = 0;

            foreach (RevenueBudget rb in revenueBudgets)
            {
                if (rb.Product.Department == department)
                {
                    totalRevenueBudget += rb.Budget;
                }
            }
            return totalRevenueBudget;
        }
    }
}
