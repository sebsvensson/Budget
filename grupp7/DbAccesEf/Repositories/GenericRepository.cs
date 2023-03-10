using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DbAccesEf.Models;

namespace DbAccesEf.Repositories
{
    public class GenericRepository<T> where T : class
    {
        public MyContext context;
        public DbSet<T> dbSet;

        public GenericRepository(MyContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        //CRUD OPERATIONS

        //Add
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        //Get multiple
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate);
        }

        //Get single
        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        //IsEmpty
        public bool IsEmpty()
        {
            return dbSet.Count() == 0;
        }

        //Remove
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        //Return all
        public IEnumerable<T> ReturnAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<RevenueBudget> ReturnAllRevenueBudgets()
        {
            return context.RevenueBudgets
                .Include(r => r.Product)
                .ThenInclude(p => p.ProductGroup)
                .ToList();
        }
        
        public IEnumerable<RevenueBudget> ReturnCustomerBudgets(string customer)
        {
            return context.Set<RevenueBudget>()
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .Where(r => r.Customer.CustomID == customer)
                .ToList();
        }

        public IEnumerable<RevenueBudget> ReturnProductBudgets(string product)
        {
            return context.Set<RevenueBudget>()
                .Include(r => r.Product)
                .Include(r => r.Customer)
                .Where(r => r.Product.CustomId == product)
                .ToList();
        }



        public List<Personell> ReturnAllPersonell()
        {
            return context.Personells
                .Include(p => p.ProductAllocations)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductGroup)
                .ToList();
        }

        public List<Account> ReturnAllAccount()
        {
            return context.Accounts
                .Include(a => a.DirectCostActivities)
                .ThenInclude(d => d.Activity)
                .Include(a => a.DirectCostProducts)
                .ThenInclude(d => d.Product)
                .ToList();
        }

        public List<DirectCostProduct> ReturnAllDirectCostProduct()
        {
            return context.DirectCostProducts
                .Include(dp => dp.Product)
                .ToList();
        }

        //Update
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
