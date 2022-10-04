﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using PresentationLayer.ViewModels;

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
                mainViewModel.SelectedViewModel = new RegisterProductViewModel();
            }
            else if(parameter.ToString() == "RegisterCustomerView")
            {
                mainViewModel.SelectedViewModel = new RegisterCustomerViewModel();
            }
            else if (parameter.ToString() == "AdministerStaffView")
            {
                mainViewModel.SelectedViewModel = new AdministerStaffViewModel();
            }
            else if (parameter.ToString() == "RevenueBudgetMenuView")
            {
                mainViewModel.SelectedViewModel = new RevenueBudgetMenuViewModel(mainViewModel);
            }
            else if (parameter.ToString() == "RevenueBudgetByCustomerView")
            {
                mainViewModel.SelectedViewModel = new RevenueBudgetByCustomerViewModel();
            }
            else if (parameter.ToString() == "ExpenseBudgetMenuView")
            {
                mainViewModel.SelectedViewModel = new ExpenseBudgetMenuViewModel();
            }
            else if (parameter.ToString() == "EditCustomerView")
            {
                mainViewModel.SelectedViewModel = new EditCustomerViewModel();
            }
            else if (parameter.ToString() == "EditProductView")
            {
                mainViewModel.SelectedViewModel = new EditProductViewModel();
            }
        }
    }
}
