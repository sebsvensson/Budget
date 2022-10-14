using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Controllers;
using DbAccesEf;
using DbAccesEf.Models;
using System.Windows.Input;
using PresentationLayer.Commands;

namespace PresentationLayer.ViewModels
{
    public class AdministerPermissionsViewModel : BaseViewModel
    {
        private UserController userController;

        //Used to check if any passwords changed
        private List<string> oldPasswords;
        private IEnumerable<User> _users;
        public IEnumerable<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(null);
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new CommandHandler(() => Save()));
            }
        }

        //Save changes
        private void Save()
        {
            System.Diagnostics.Debug.WriteLine(Users.First().PassWord);
            System.Diagnostics.Debug.WriteLine(oldPasswords.First());

            //Check which passwords changed          
            for (int i = 0; i < Users.Count(); i++)
            {
                if (Users.ElementAt(i).PassWord != oldPasswords.ElementAt(i))
                {
                    System.Diagnostics.Debug.WriteLine(i);
                    userController.EditPassword(Users.ElementAt(i).UserID, Users.ElementAt(i).PassWord);
                }
            }
        }

        public AdministerPermissionsViewModel()
        {
            userController = new UserController(new MyContext());

            //Fill datagrid
            Users = userController.GetAll();

            //Clone list
            oldPasswords = new List<string>();
            foreach(User u in Users)
            {
                System.Diagnostics.Debug.WriteLine(u.PassWord);
                oldPasswords.Add(u.PassWord);
            }
        }
    }
}
