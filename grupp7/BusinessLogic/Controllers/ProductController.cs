using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf;
using DbAccesEf.Models;

namespace BusinessLogic.Controllers
{
    public class ProductController
    {
        private UnitOfWork unitOfWork;
        private AccountController accountController;

        public ProductController(MyContext context) 
        {
            unitOfWork = new UnitOfWork(context);
            accountController = new AccountController(context);
        }

        //Register product
        public void RegisterProduct(string productName, string xxxx, ProductCategory productCategory, ProductGroup productGroup)
        {
            //Create custom id
            string customId = xxxx + productGroup.Name.Substring(0, 2);
                    
            unitOfWork.ProductRepository.Add(new DbAccesEf.Models.Product()
            {
                CustomId = customId,
                Xxxx = xxxx,
                ProductName = productName,
                ProductGroup = productGroup,
                ProductCategory = productCategory
            });

            unitOfWork.SaveChanges();

            accountController.GenerateProductDirectCostZeroes(GetByProductName(productName));
        }

        public Product GetByProductName(string productName)
        {
            return unitOfWork.ProductRepository.FirstOrDefault(p => p.ProductName == productName);
        }

        //Get all productgroups
        public IEnumerable<ProductGroup> GetAllProductGroups()
        {
            return unitOfWork.ProductGroupRepository.ReturnAll();
        }

        //Get all productcategories
        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            return unitOfWork.ProductCategoryRepository.ReturnAll();
        }

        public IEnumerable<Product> GetAll()
        {
            return unitOfWork.ProductRepository.ReturnAll();
        }

        public IEnumerable<Product> GetByDepartment(string department)
        {
            if(department == "Drift")
            {
                return unitOfWork.ProductRepository.Find(p => p.Department == "Drift");
            }
            else if (department == "Utv/Förv")
            {
                return unitOfWork.ProductRepository.Find(p => p.Department == "Utv/Förv");
            }
            else
            {
                return null;
            }
        }

        //Add new productgroup
        public void AddProductGroup(string name)
        {
            unitOfWork.ProductGroupRepository.Add(new ProductGroup(name));
            unitOfWork.SaveChanges();
        }

        public void AddProductCategory(string name)
        {
            
            unitOfWork.ProductCategoryRepository.Add(new ProductCategory(name));
            unitOfWork.SaveChanges();
        }
    }
}
