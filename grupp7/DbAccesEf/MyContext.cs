using System;
using System.Collections.Generic;
using System.Text;
using DbAccessEf.Models;
using Microsoft.EntityFrameworkCore;

namespace DbAccessEf
{
    public class MyContext : DbContext
    {
        private string connectionString = "Server=sqlutb2-db.hb.se,56077;Database=suht2207;user id=suht2207;password=maxx99;";
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public void Configure()
        {
            Database.EnsureCreated();
        }
    }
}
