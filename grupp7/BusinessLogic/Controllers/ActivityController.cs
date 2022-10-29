using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf;
using DbAccesEf.Models;

namespace BusinessLogic.Controllers
{
    public class ActivityController
    {
        private UnitOfWork unitOfWork;
        private AccountController accountController;

        public ActivityController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
            accountController = new AccountController(context);
        }

        //Register Activity
        public void RegisterActivity(string activityName, string activityXxxx, string aFFODepartment, string customID)
        {
            unitOfWork.ActivityRepository.Add(new Activity()
            {
                CustomID = customID,
                ActivityXxxx = activityXxxx,
                ActivityName = activityName,
                AFFODepartment = aFFODepartment
            });

            unitOfWork.SaveChanges();
            accountController.GenerateActivityDirectCostZeroes(GetByCustomID(customID));
        }

        public Activity GetByCustomID(string customID)
        {
            return unitOfWork.ActivityRepository.FirstOrDefault(a => a.CustomID == customID);
        }

        public IEnumerable<Activity> GetByDepartment(string department)
        {
            if (department == "AO")
            {
                return unitOfWork.ActivityRepository.Find(a => a.AFFODepartment == "AO");
            }
            else if (department == "FO")
            {
                return unitOfWork.ActivityRepository.Find(a => a.AFFODepartment == "FO");
            }
            else
            {
                return null;
            }
        }
    }
}
