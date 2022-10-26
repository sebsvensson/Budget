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
        }
        public ProductCategory GetProductCategory(string name)
        {
            return unitOfWork.ProductCategoryRepository.FirstOrDefault(c => c.Name == name);
        }
        public ProductGroup GetProductGroup(string name)
        {
            return unitOfWork.ProductGroupRepository.FirstOrDefault(c => c.Name == name);
        }

        //Get all productgroups
        public IEnumerable<ProductGroup> GetAllProductGroups()
        {
            return unitOfWork.ProductGroupRepository.ReturnAll();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return unitOfWork.ProductRepository.ReturnAll();
        }

        //Get all productcategories
        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            return unitOfWork.ProductCategoryRepository.ReturnAll();
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

        public Product GetByID(string ID)
        {
            return unitOfWork.ProductRepository.FirstOrDefault(p => p.CustomId == ID);
        }

        public void EditProduct(string customID, string name, string xxxx, string productGroup, string productCategory)
        {
            Product product = unitOfWork.ProductRepository.FirstOrDefault(p => p.CustomId == customID);
            customID = xxxx + productGroup.Substring(0, 2);
            product.CustomId = customID;
            product.ProductName = name;
            product.Xxxx = xxxx;
            product.ProductCategory.Name = productCategory;
            product.ProductGroup.Name = productGroup;

            unitOfWork.SaveChanges();
        }
    }
}
