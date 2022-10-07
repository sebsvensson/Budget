using System;
using System.Collections.Generic;
using System.Text;
using DBAccessEF.Repositories;
using DBAccessEF.Models;

namespace DBAccessEF
{
    public class UnitOfWork
    {
        private readonly MyContext context;

        //REPOSITORIES
        public GenericRepository<Product> ProductRepository { get; set; }


        public UnitOfWork(MyContext context)
        {
            this.context = context;
            ProductRepository = new GenericRepository<Product>(context);
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
