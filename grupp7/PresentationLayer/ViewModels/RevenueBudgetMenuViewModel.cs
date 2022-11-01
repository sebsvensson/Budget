using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresentationLayer.Commands;
using BusinessLogic.Controllers;

namespace PresentationLayer.ViewModels
{
    public class RevenueBudgetMenuViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        private BudgetLockController budgetLockController;
        public ICommand UpdateViewCommand { get; set; }
        public RevenueBudgetMenuViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            UpdateViewCommand = new UpdateViewCommand(this.mainViewModel);
            budgetLockController = new BudgetLockController(new DbAccesEf.MyContext());
            SetLockText();

            if (mainViewModel.loggedInUser.PermissionLevel != "CE" && mainViewModel.loggedInUser.PermissionLevel != "CFOM")
            {
                LockEnabled = false;
            }
            else
            {
                LockEnabled = true;
            }
        }

        private string _lockText;
        public string LockText
        {
            get { return _lockText; }
            set
            {
                _lockText = value;
                OnPropertyChanged(null);
            }
        }

        private bool _lockEnabled;
        public bool LockEnabled
        {
            get { return _lockEnabled; }
            set
            {
                _lockEnabled = value;
                OnPropertyChanged(null);
            }
        }

        private void SetLockText()
        {
            if (budgetLockController.GetRevenueBudgetLocked())
            {
                LockText = "Lås budget";
            }
            else
            {
                LockText = "Lås upp budget";
            }
        }

        private ICommand _lockBudgetCommand;
        public ICommand LockBudgetCommand
        {
            get
            {
                return _lockBudgetCommand ?? (_lockBudgetCommand = new CommandHandler(() => LockBudget()));
            }
        }

        private void LockBudget()
        {
            bool isLocked = budgetLockController.GetRevenueBudgetLocked();
            budgetLockController.SetRevenueBudgetLock(!isLocked);
            SetLockText();
        }
    }
}
