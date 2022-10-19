using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresentationLayer.Commands;
using PresentationLayer.ViewModels;
using DbAccesEf.Models;
using DbAccesEf;
using BusinessLogic.Controllers;


namespace PresentationLayer.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private MyContext context;
        private UserController userController;
        private ResourceController resourceController;

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(null);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(null);
            }
        }

        private MainViewModel mainViewModel;
        private ICommand _loginCommand;
        public ICommand LogInCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new CommandHandler(() => LogIn()));
            }
        }

        private ICommand _readExcelCommand;
        public ICommand ReadExcelCommand
        {
            get
            {
                return _readExcelCommand ?? (_readExcelCommand = new CommandHandler(() => ReadExcel()));
            }
        }

        public LoginViewModel(MainViewModel mainViewModel)
        {
            context = new MyContext();
            userController = new UserController(context);
            resourceController = new ResourceController(context);
            this.mainViewModel = mainViewModel;
        }
        private void LogIn()
        {
            if (userController.LogIn(UserName, Password) != null)
            {
                mainViewModel.loggedInUser = userController.LogIn(UserName, Password);

                mainViewModel.SelectedViewModel = new TestViewModel();
                mainViewModel.ColumnSpan = 2;
                mainViewModel.GridColumn = 2;
                mainViewModel.GridRow = 2;

                mainViewModel.LoggedInText = "Inloggad som: " + userController.LogIn(UserName, Password).UserName;
            }
        }

        private void ReadExcel()
        {
            resourceController.ReadExcelProductCategoryGroup("Produkter.xlsx");
            resourceController.ReadExcelProduct("Produkter.xlsx");
            resourceController.ReadExcelPersonell("Personallista.xlsx");
            resourceController.ReadExcelAccount("Kontoplan.xlsx");
        }
    }
}
