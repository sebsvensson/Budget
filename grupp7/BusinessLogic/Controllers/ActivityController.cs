using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf;

namespace BusinessLogic.Controllers
{
    public class ActivityController
    {
        private UnitOfWork unitOfWork;

        public ActivityController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        //metoder
    }
}
