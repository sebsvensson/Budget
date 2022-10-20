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
            return unitOfWork.PersonellRepository.ReturnAll();
        }
    }
}
