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

        public void ReadExcelProductCategoryGroup(string filePath)
        {
            DataTable excelData = generateData.ExcelToDataTable(filePath);
            
            
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

                if (!uniqueGroups.Any(c => c == excelData.Rows[i].Field<string>(2)))
                {
                    uniqueGroups.Add(excelData.Rows[i].Field<string>(2));
                }
            }

            //Add uniqueCategories and uniqueGroups to database
            if (unitOfWork.ProductGroupRepository.IsEmpty())
            {
                foreach (string group in uniqueGroups)
                {
                    unitOfWork.ProductGroupRepository.Add(new ProductGroup() { Name = group });
                }
            }
            if (unitOfWork.ProductCategoryRepository.IsEmpty())
            {
                foreach (string category in uniqueCategories)
                {
                    unitOfWork.ProductCategoryRepository.Add(new ProductCategory() { Name = category });
                }
            }

            unitOfWork.SaveChanges();
        }
    }
}
