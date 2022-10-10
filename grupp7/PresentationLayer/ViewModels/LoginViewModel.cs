using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresentationLayer.Commands;
using PresentationLayer.ViewModels;
using DbAccessEf.Models;


namespace PresentationLayer.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        private ICommand _loginCommand;
        public ICommand LogInCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new CommandHandler(() => LogIn()));
            }
        }

        public LoginViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }
        private void LogIn()
        {
            mainViewModel.SelectedViewModel = new TestViewModel();
            mainViewModel.ColumnSpan = 2;
            mainViewModel.GridColumn = 2;
            mainViewModel.GridRow = 2;
            
            DbAccessEf.Models.Product test = new DbAccessEf.Models.Product() { ProductCategory = "hej", ProductGroup = "hej", ProductName = "hej" };
            App.unitOfWork.ProductRepository.Add(test);
            App.unitOfWork.SaveChanges();
        }        
    }
}
