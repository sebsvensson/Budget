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

        public ProductController(MyContext context) 
        {
            unitOfWork = new UnitOfWork(context);
        }

        //Register product
        public void RegisterProduct(string productName, string xxxx, ProductCategory productCategory, ProductGroup productGroup)
        {
            //Create custom id
            Product product = new Product();
           string customId = xxxx + productGroup.Name.Substring(0, 1);
                    
            unitOfWork.ProductRepository.Add(new DbAccesEf.Models.Product()
            {
                CustomId = customId,
                Xxxx = xxxx,
                ProductName = productName,
                ProductGroup = productGroup,
                ProductCategory = productCategory
            });

            unitOfWork.SaveChanges();
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

        //Add new productgroup
        public void AddProductGroup(string name)
        {
            unitOfWork.ProductGroupRepository.Add(new ProductGroup() { Name = name });
            unitOfWork.SaveChanges();
        }

        public void AddProductCategory(string name)
        {
            
            unitOfWork.ProductCategoryRepository.Add(new ProductCategory() { Name = name });
            unitOfWork.SaveChanges();
        }
        
        public IEnumerable<Product> GetAllProducts()
        {
            return unitOfWork.ProductRepository.ReturnAll();
        }

    }
}
