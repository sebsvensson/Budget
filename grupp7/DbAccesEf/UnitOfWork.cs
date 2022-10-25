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
        public GenericRepository<User> UserRepository { get; set; }
        public GenericRepository<RevenueBudget> RevenueBudgetRepository { get; set; }
        public GenericRepository<Personell> PersonellRepository { get; set; }
        public GenericRepository<Account> AccountRepository { get; set; }
        public GenericRepository<ProductAllocation> ProductAllocationRepository { get; set; }

        public UnitOfWork(MyContext context)
        {
            this.context = context;
            context.Database.EnsureCreated();
            ProductRepository = new GenericRepository<Product>(context);
            ProductGroupRepository = new GenericRepository<ProductGroup>(context);
            ProductCategoryRepository = new GenericRepository<ProductCategory>(context);
            CustomerRepository = new GenericRepository<Customer>(context);
            CustomerCategoryRepository = new GenericRepository<CustomerCategory>(context);
            ActivityRepository = new GenericRepository<Activity>(context);
            UserRepository = new GenericRepository<User>(context);
            RevenueBudgetRepository = new GenericRepository<RevenueBudget>(context);
            PersonellRepository = new GenericRepository<Personell>(context);
            AccountRepository = new GenericRepository<Account>(context);
            ProductAllocationRepository = new GenericRepository<ProductAllocation>(context);
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
