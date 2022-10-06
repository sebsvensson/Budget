using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresentationLayer.Commands;

namespace PresentationLayer.ViewModels
{
    public class ExpenseBudgetMenuViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        public ICommand UpdateViewCommand { get; set; }
        public ExpenseBudgetMenuViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            UpdateViewCommand = new UpdateViewCommand(mainViewModel);
        }
    }
}
