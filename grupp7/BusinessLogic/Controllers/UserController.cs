using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf;
using DbAccesEf.Models;

namespace BusinessLogic.Controllers
{
    public class UserController
    {
        private UnitOfWork unitOfWork;

        public UserController(MyContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<User> GetAll()
        {
            return unitOfWork.UserRepository.ReturnAll();
        }

        public void AddUser(string userName, string password, string permissionLevel)
        {
            User newUser = new User()
            {
                UserName = userName,
                PassWord = password,
                PermissionLevel = permissionLevel
            };

            unitOfWork.UserRepository.Add(newUser);
            unitOfWork.SaveChanges();
        }

        public User LogIn(string userName, string passWord)
        {
            User user = unitOfWork.UserRepository.FirstOrDefault(u => u.UserName == userName);
            
            if(user != null && user.PassWord == passWord)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public void EditPassword(int ID, string newPassword)
        {
            User user = unitOfWork.UserRepository.FirstOrDefault(u => u.UserID == ID);
            user.PassWord = newPassword;
            unitOfWork.SaveChanges();
        }
    }
}
