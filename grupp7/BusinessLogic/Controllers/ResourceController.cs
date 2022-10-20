using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf;
using DbAccesEf.Models;

namespace BusinessLogic.Controllers
{
    public class ResourceController
    {
        private UnitOfWork unitOfWork;
        private GenerateData generateData;

        public ResourceController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
            generateData = new GenerateData();
        }

        //Reads product category and groups and fills database
        public void ReadExcelProductCategoryGroup(string fileName)
        {
            DataTable excelData = generateData.ExcelToDataTable(fileName);
                    
            //Put into database
            List<string> uniqueCategories = new List<string>();
            List<string> uniqueGroups = new List<string>();

            for(int i = 0; i < excelData.Rows.Count; i++)
            {
                //if not already exists
                if(!uniqueCategories.Any(c => c == excelData.Rows[i].Field<string>(3)))
                {
                    uniqueCategories.Add(excelData.Rows[i].Field<string>(3));
                }

                if (!uniqueGroups.Any(g => g == excelData.Rows[i].Field<string>(2)))
                {
                    uniqueGroups.Add(excelData.Rows[i].Field<string>(2));
                }
            }

            //Add uniqueCategories and uniqueGroups to database
            if (unitOfWork.ProductGroupRepository.IsEmpty())
            {
                foreach (string group in uniqueGroups)
                {
                    unitOfWork.ProductGroupRepository.Add(new ProductGroup(group));
                }
            }
            if (unitOfWork.ProductCategoryRepository.IsEmpty())
            {
                foreach (string category in uniqueCategories)
                {
                    unitOfWork.ProductCategoryRepository.Add(new ProductCategory(category));
                }
            }

            unitOfWork.SaveChanges();
        }

        //Read products in fills database
        public void ReadExcelProduct(string fileName)
        {
            DataTable excelData = generateData.ExcelToDataTable(fileName);

            //Creates lists of products
            List<Product> newProducts = new List<Product>();
            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                newProducts.Add(new Product()
                {
                    CustomId = excelData.Rows[i].Field<string>(0),
                    Xxxx = excelData.Rows[i].Field<string>(0).Substring(0, 5),
                    ProductName = excelData.Rows[i].Field<string>(1),
                    ProductCategory = new ProductCategory(excelData.Rows[i].Field<string>(3)),
                    ProductGroup = new ProductGroup(excelData.Rows[i].Field<string>(2)),
                    Department = excelData.Rows[i].Field<string>(4)
                });
            }

            //Puts products list in database
            if (unitOfWork.ProductRepository.IsEmpty())
            {
                foreach(Product p in newProducts)
                {
                    unitOfWork.ProductRepository.Add(p);
                }
            }

            unitOfWork.SaveChanges();
        }

        public void ReadExcelPersonell(string fileName)
        {
            DataTable excelData = generateData.ExcelToDataTable(fileName);

            //Creates list of Personell
            List<Personell> newPersonell = new List<Personell>();
            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                newPersonell.Add(new Personell()
                {
                    Pnr = excelData.Rows[i].Field<string>(0),
                    Name = excelData.Rows[i].Field<string>(1),
                    MonthlySalary = CheckDBNull(excelData.Rows[i][2]),
                    EmploymentRate = CheckDBNull(excelData.Rows[i][3]),
                    VacancyDeduction = CheckDBNull(excelData.Rows[i][4]),
                    AnnualWorkRate = CheckDBNull(excelData.Rows[i][5]),
                    Adm = CheckDBNull(excelData.Rows[i][6]),
                    ForsMark = CheckDBNull(excelData.Rows[i][7]),
                    UtvForv = CheckDBNull(excelData.Rows[i][8]),
                    Drift = CheckDBNull(excelData.Rows[i][9]),
                    ProductAllocations = GenerateProductAllocationZeroes()
                });
            }

            if (unitOfWork.PersonellRepository.IsEmpty())
            {
                foreach (Personell p in newPersonell)
                {
                    unitOfWork.PersonellRepository.Add(p);
                }
            }

            unitOfWork.SaveChanges();
        }

        //Generate ProductAllocation zeroes on personell
        private List<ProductAllocation> GenerateProductAllocationZeroes()
        {
            List<ProductAllocation> result = new List<ProductAllocation>();

            foreach(Product p in unitOfWork.ProductRepository.ReturnAll())
            {
                result.Add(new ProductAllocation()
                {
                    Allocation = 0,
                    Product = p
                });
            }

            return result;
        }

        //Checks if DataTable cell is null, if yes: returns 0
        private double CheckDBNull(object input)
        {
            if (input == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(input);
            }
        }

        public void ReadExcelAccount(string fileName)
        {
            DataTable excelData = generateData.ExcelToDataTable(fileName);

            //Creates list of Accounts
            List<Account> newAccounts = new List<Account>();

            System.Diagnostics.Debug.WriteLine(excelData.Rows[0][0].GetType());
            System.Diagnostics.Debug.WriteLine(excelData.Rows[0][1].GetType());

            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                newAccounts.Add(new Account()
                {
                    AccountNumber = Convert.ToInt32(excelData.Rows[i].Field<string>(0)),
                    Name = excelData.Rows[i].Field<string>(1),
                    SchablonExpense = 0
                });
            }

            if (unitOfWork.AccountRepository.IsEmpty())
            {
                foreach (Account a in newAccounts)
                {
                    unitOfWork.AccountRepository.Add(a);
                }

            }

            unitOfWork.SaveChanges();
        }
    }
}
