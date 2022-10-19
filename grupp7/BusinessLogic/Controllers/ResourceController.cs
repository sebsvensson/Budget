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

        public void ReadExcelProduct(string fileName)
        {
            DataTable excelData = generateData.ExcelToDataTable(fileName);

            List<Product> newProducts = new List<Product>();

            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                newProducts.Add(new Product()
                {
                    CustomId = excelData.Rows[i].Field<string>(0),
                    Xxxx = excelData.Rows[i].Field<string>(0).Substring(0, 5),
                    ProductName = excelData.Rows[i].Field<string>(1),
                    ProductCategory = new ProductCategory(excelData.Rows[i].Field<string>(3)),
                    ProductGroup = new ProductGroup(excelData.Rows[i].Field<string>(2))
                });
            }

            if (unitOfWork.ProductRepository.IsEmpty())
            {
                foreach(Product p in newProducts)
                {
                    unitOfWork.ProductRepository.Add(p);
                }
            }

            unitOfWork.SaveChanges();
        }
    }
}
