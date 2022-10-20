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
        private readonly UnitOfWork unitOfWork;

        public CustomerController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public void RegisterCustomer(string customerID, string customerName, CustomerCategory category)
        {

            Customer customer = new Customer();

            unitOfWork.CustomerRepository.Add(new DbAccesEf.Models.Customer()
            {
                CustomID = customerID,
                CustomerName = customerName,
                Category = category
                
            });

            unitOfWork.SaveChanges();
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

        public void AttachCategory(CustomerCategory category)
        {
            Customer customer = new Customer();
            customer.Category.Equals(category);
        }
        public Customer GetByID(string id)
        {
            return unitOfWork.CustomerRepository.FirstOrDefault(c => c.CustomID == id);
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
