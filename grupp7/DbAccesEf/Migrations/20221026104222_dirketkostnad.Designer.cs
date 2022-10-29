﻿// <auto-generated />
using System;
using DbAccesEf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DbAccesEf.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20221026104222_dirketkostnad")]
    partial class dirketkostnad
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DbAccesEf.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SchablonExpense")
                        .HasColumnType("float");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("DbAccesEf.Models.Activity", b =>
                {
                    b.Property<int>("ActivityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AFFODepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActivityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActivityXxxx")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActivityID");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("DbAccesEf.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryCustomerCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("CustomID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.HasIndex("CategoryCustomerCategoryID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DbAccesEf.Models.CustomerCategory", b =>
                {
                    b.Property<int>("CustomerCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerCategoryID");

                    b.ToTable("CustomerCategories");
                });

            modelBuilder.Entity("DbAccesEf.Models.DirectCostActivity", b =>
                {
                    b.Property<int>("DirectCostActivityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("ActivityID")
                        .HasColumnType("int");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.HasKey("DirectCostActivityID");

                    b.HasIndex("AccountId");

                    b.HasIndex("ActivityID");

                    b.ToTable("DirectCostActivities");
                });

            modelBuilder.Entity("DbAccesEf.Models.DirectCostProduct", b =>
                {
                    b.Property<int>("DirectCostProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("DirectCostProductID");

                    b.HasIndex("AccountId");

                    b.HasIndex("ProductID");

                    b.ToTable("DirectCostProducts");
                });

            modelBuilder.Entity("DbAccesEf.Models.Personell", b =>
                {
                    b.Property<int>("PersonellID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Adm")
                        .HasColumnType("float");

                    b.Property<double>("AnnualWorkRate")
                        .HasColumnType("float");

                    b.Property<double>("Drift")
                        .HasColumnType("float");

                    b.Property<double>("EmploymentRate")
                        .HasColumnType("float");

                    b.Property<double>("ForsMark")
                        .HasColumnType("float");

                    b.Property<double>("MonthlySalary")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pnr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("UtvForv")
                        .HasColumnType("float");

                    b.Property<double>("VacancyDeduction")
                        .HasColumnType("float");

                    b.HasKey("PersonellID");

                    b.ToTable("Personells");
                });

            modelBuilder.Entity("DbAccesEf.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductCategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductGroupID")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Xxxx")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.HasIndex("ProductCategoryId");

                    b.HasIndex("ProductGroupID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DbAccesEf.Models.ProductAllocation", b =>
                {
                    b.Property<int>("ProductAllocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Allocation")
                        .HasColumnType("float");

                    b.Property<int>("PersonellID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ProductAllocationID");

                    b.HasIndex("PersonellID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductAllocations");
                });

            modelBuilder.Entity("DbAccesEf.Models.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductCategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("DbAccesEf.Models.ProductGroup", b =>
                {
                    b.Property<int>("ProductGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductGroupID");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("DbAccesEf.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PassWord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermissionLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DbAccesEf.Models.Customer", b =>
                {
                    b.HasOne("DbAccesEf.Models.CustomerCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryCustomerCategoryID");
                });

            modelBuilder.Entity("DbAccesEf.Models.DirectCostActivity", b =>
                {
                    b.HasOne("DbAccesEf.Models.Account", null)
                        .WithMany("DirectCostActivities")
                        .HasForeignKey("AccountId");

                    b.HasOne("DbAccesEf.Models.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityID");
                });

            modelBuilder.Entity("DbAccesEf.Models.DirectCostProduct", b =>
                {
                    b.HasOne("DbAccesEf.Models.Account", null)
                        .WithMany("DirectCostProducts")
                        .HasForeignKey("AccountId");

                    b.HasOne("DbAccesEf.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("DbAccesEf.Models.Product", b =>
                {
                    b.HasOne("DbAccesEf.Models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryId");

                    b.HasOne("DbAccesEf.Models.ProductGroup", "ProductGroup")
                        .WithMany()
                        .HasForeignKey("ProductGroupID");
                });

            modelBuilder.Entity("DbAccesEf.Models.ProductAllocation", b =>
                {
                    b.HasOne("DbAccesEf.Models.Personell", null)
                        .WithMany("ProductAllocations")
                        .HasForeignKey("PersonellID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbAccesEf.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");
                });
#pragma warning restore 612, 618
        }
    }
}
