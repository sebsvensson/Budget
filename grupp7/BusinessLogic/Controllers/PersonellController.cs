using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf;
using DbAccesEf.Models;

namespace BusinessLogic.Controllers
{
    public class PersonellController
    {
        private UnitOfWork unitOfWork;
        public PersonellController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<Personell> GetAll()
        {
            return unitOfWork.PersonellRepository.ReturnAllPersonell();
        }

        public IEnumerable<ProductAllocation> GetProductAllocations(int personellID)
        {
            return unitOfWork.ProductAllocationRepository.Find(p => p.PersonellID == personellID);
        }

        public void EditProductAllocation(double newAllocation, int personellID, int productID)
        {
            Personell personell = GetAll().ToList().FirstOrDefault(p => p.PersonellID == personellID);
            ProductAllocation productAllocation = personell.ProductAllocations.FirstOrDefault(pa => pa.Product.ProductID == productID);
            productAllocation.Allocation = newAllocation;
            unitOfWork.SaveChanges();
        }
    }
}
