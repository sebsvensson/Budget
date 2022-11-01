using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using PresentationLayer.ViewModels;
using PresentationLayer.Utilities;

namespace PresentationLayer.Commands
{
    public class UpdateViewCommand : ICommand
    {

        private MainViewModel mainViewModel;

        public UpdateViewCommand(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        //Sets SelectedViewModel based on CommandParameter from xaml view
        public void Execute(object parameter)
        {
            mainViewModel.ViewAccesed = true;

            //Stores last view before changing view
            mainViewModel.viewQueueHandler.NewView(mainViewModel.SelectedViewModel);

            //Acces bug with viewqueuehandler
            //Fix: only change view if acces, else change current view to ViewAccesed = false AND set ViewAccesed to true in ViewQueueHandler


            if (parameter.ToString() == "HomeView")
            {
                mainViewModel.SelectedViewModel = new TestViewModel();
            }
            else if (parameter.ToString() == "ForeCastingView")
            {
                 mainViewModel.SelectedViewModel = new ForeCastingViewModel();
            }
            else if (parameter.ToString() == "RegisterProductView")
            {

                if(mainViewModel.loggedInUser.PermissionLevel == "MFM")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new RegisterProductViewModel();
                }
            }
            else if(parameter.ToString() == "RegisterCustomerView")
            {
                if (mainViewModel.loggedInUser.PermissionLevel != "CFOM" && mainViewModel.loggedInUser.PermissionLevel != "CE")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new RegisterCustomerViewModel();
                }
            }
            else if (parameter.ToString() == "AdministerStaffView")
            {
                if (mainViewModel.loggedInUser.PermissionLevel != "CPA" && mainViewModel.loggedInUser.PermissionLevel != "CE")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new AdministerStaffViewModel();
                }
            }
            else if (parameter.ToString() == "RevenueBudgetMenuView")
            {
                if(mainViewModel.loggedInUser.PermissionLevel != "CE" && mainViewModel.loggedInUser.PermissionLevel != "CFOM" && mainViewModel.loggedInUser.PermissionLevel != "MFM")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new RevenueBudgetMenuViewModel(mainViewModel);
                }
            }
            else if (parameter.ToString() == "RevenueBudgetByCustomerView")
            {
                mainViewModel.SelectedViewModel = new RevenueBudgetByCustomerViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "AddRevenueByCustomerView")
            {
                mainViewModel.SelectedViewModel = new AddRevenueBudgetByCustomerViewModel();
            }
            else if (parameter.ToString() == "RevenueBudgetByProductView")
            {
                mainViewModel.SelectedViewModel = new RevenueBudgetByProductViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "AddRevenueByProductView")
            {
                mainViewModel.SelectedViewModel = new AddRevenueByProductViewModel();
            }
            if (parameter.ToString() == "ExpenseBudgetMenuView")
            {
                mainViewModel.SelectedViewModel = new ExpenseBudgetMenuViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "EditCustomerView")
            {
                if(mainViewModel.loggedInUser.PermissionLevel != "CE")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new EditCustomerViewModel();
                }
            }
            else if (parameter.ToString() == "EditProductView")
            {
                if (mainViewModel.loggedInUser.PermissionLevel != "CE")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new EditProductViewModel();
                }
            }
            else if (parameter.ToString() == "EditActivityView")
            {
                if (mainViewModel.loggedInUser.PermissionLevel != "CE")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new EditActivityViewModel();
                }
            }
            else if (parameter.ToString() == "RegisterActivityView")
            {
                if(mainViewModel.loggedInUser.PermissionLevel == "MFM")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new RegisterActivityViewModel();
                }
            }
            else if (parameter.ToString() == "AdministerPermissionsView")
            {
                if(mainViewModel.loggedInUser.PermissionLevel != "CE")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new AdministerPermissionsViewModel();
                }
            }
            else if (parameter.ToString() == "BudgetResultView")
            {
                mainViewModel.SelectedViewModel = new BudgetResultViewModel();
            }

            else if (parameter.ToString() == "SchablonExpenseView")
            {
                if (mainViewModel.loggedInUser.PermissionLevel != "CE")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new SchablonExpenseViewModel();
                }
            }
            else if (parameter.ToString() == "ResourceAllocationView")
            {
                if (mainViewModel.loggedInUser.PermissionLevel == "MFM")
                {
                    mainViewModel.ViewAccesed = false;
                }
                else
                {
                    mainViewModel.SelectedViewModel = new ResourceAllocation2ViewModel();
                }
            }
            else if (parameter.ToString() == "DirectCostProductView")
            {
                mainViewModel.SelectedViewModel = new DirectCostProductViewModel(mainViewModel);

            }
            else if (parameter.ToString() == "DirectCostActivityView")
            {
                mainViewModel.SelectedViewModel = new DirectCostActivityViewModel();
            }
        }
    }
}
