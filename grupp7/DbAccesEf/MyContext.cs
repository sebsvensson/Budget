using System;
using System.Collections.Generic;
using System.Text;
using DbAccesEf.Models;
using Microsoft.EntityFrameworkCore;

namespace DbAccesEf
{
    public class MyContext : DbContext
    {
        private string connectionString = "Server=sqlutb2-db.hb.se,56077;Database=suht2207;user id=suht2207;password=maxx99;";
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
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
