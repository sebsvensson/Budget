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
            else if (parameter.ToString() == "MyBookings")
            {

            }
        }
    }
}