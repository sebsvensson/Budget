using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic.Controllers;
using DbAccesEf;
using DbAccesEf.Models;
using PresentationLayer.Commands;

namespace PresentationLayer.ViewModels
{
    public class ExpenseBudgetMenuViewModel : BaseViewModel
    {
        private MainViewModel mainViewModel;
        private YieldController yieldController;
        private MyContext context;
        public ICommand UpdateViewCommand { get; set; }

        public ExpenseBudgetMenuViewModel(MainViewModel mainViewModel)
        {
            context = new MyContext();
            yieldController = new YieldController(context);
            this.mainViewModel = mainViewModel;
            UpdateViewCommand = new UpdateViewCommand(mainViewModel);
            ShowYield();
        }

        public void ShowYield()
        {
            Yield yield = yieldController.GetYield();
            Amount = yield.Amount;
        }

        public void UpdateYield()
        {
            yieldController.EditYield(Amount);
        }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        private Yield _yield;
        public Yield Yield
        {
            get { return _yield; }
            set
            {
                _yield = value;
                OnPropertyChanged();
            }
        }

        private ICommand _updateYieldCommand;
        public ICommand UpdateYieldCommand
        {
            get
            {
                return _updateYieldCommand ?? (_updateYieldCommand = new CommandHandler(() => UpdateYield()));
            }
        }
    }
}
