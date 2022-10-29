using DbAccesEf;
using DbAccesEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Controllers
{
    public class CustomerController
    {
        private UnitOfWork unitOfWork;

        public CustomerController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void RegisterCustomer(string customID, string customerName, CustomerCategory category)
        {

            Customer customer = new Customer();

            unitOfWork.CustomerRepository.Add(new DbAccesEf.Models.Customer()
            {
                CustomID = customID,
                CustomerName = customerName,
                Category = category,
                
            });

            unitOfWork.SaveChanges();
        }
        public CustomerCategory GetCustomerCategory(string name)
        {
            return unitOfWork.CustomerCategoryRepository.FirstOrDefault(c => c.Name == name);
        }
       
        public IEnumerable<CustomerCategory> GetCustomerCategories()
        {
            return unitOfWork.CustomerCategoryRepository.ReturnAll();
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            return unitOfWork.CustomerRepository.ReturnAll();
        }

        public void AddCustomerCategory(string name)
        {
            unitOfWork.CustomerCategoryRepository.Add(new CustomerCategory() { Name = name });
            unitOfWork.SaveChanges();
        }

        

        public Customer GetByID(string ID)
        {
            return unitOfWork.CustomerRepository.FirstOrDefault(p => p.CustomID == ID);
        }
        
        public void EditCustomer(string customID, string name, string category)
        {
            Customer customer = unitOfWork.CustomerRepository.FirstOrDefault(c => c.CustomID == customID);
            customer.CustomID = customID;
            customer.CustomerName = name;
            customer.Category.Name = category;
            
            unitOfWork.SaveChanges();
        }

    }
}
