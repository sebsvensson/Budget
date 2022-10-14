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

        //Register Activity
        public void RegisterActivity(string activityName, string activityXxxx, string aFFODepartment, string customID)
        {

            unitOfWork.ActivityRepository.Add(new DbAccesEf.Models.Activity()
            {
                CustomID = customID,
                ActivityXxxx = activityXxxx,
                ActivityName = activityName,
                AFFODepartment = aFFODepartment
            });

            unitOfWork.SaveChanges();
        }
    }
}
