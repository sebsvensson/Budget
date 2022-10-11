using System;
using DbAccesEf;
using DbAccesEf.Repositories;
using DbAccesEf.Models;
using BusinessLogic.Controllers;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MyContext contextTest = new MyContext();
            contextTest.Database.EnsureCreated();

            ProductController controller = new ProductController(contextTest);
            controller.AddProductGroup("test");

        }
    }
}
