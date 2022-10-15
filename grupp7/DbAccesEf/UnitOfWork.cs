using System;
using System.Collections.Generic;
using System.Text;
using DbAccesEf.Repositories;
using DbAccesEf.Models;

namespace DbAccesEf
{
    public class UnitOfWork
    {
        private readonly MyContext context;

        //REPOSITORIES
        public GenericRepository<Product> ProductRepository { get; set; }
        public GenericRepository<ProductGroup> ProductGroupRepository { get; set; }
        public GenericRepository<ProductCategory> ProductCategoryRepository { get; set; }
        public GenericRepository<Customer> CustomerRepository { get; set; }
        public GenericRepository<CustomerCategory> CustomerCategoryRepository { get; set; }
        public GenericRepository<Activity> ActivityRepository { get; set; }

        public UnitOfWork(MyContext context)
        {
            this.context = context;
            ProductRepository = new GenericRepository<Product>(context);
            ProductGroupRepository = new GenericRepository<ProductGroup>(context);
            ProductCategoryRepository = new GenericRepository<ProductCategory>(context);
            CustomerRepository = new GenericRepository<Customer>(context);
            CustomerCategoryRepository = new GenericRepository<CustomerCategory>(context);
            ActivityRepository = new GenericRepository<Activity>(context);
        }

        public void SaveChanges()
        {

            try
            {
                context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
