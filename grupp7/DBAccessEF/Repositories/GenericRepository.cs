using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DBAccessEF.Repositories
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
    }
}
