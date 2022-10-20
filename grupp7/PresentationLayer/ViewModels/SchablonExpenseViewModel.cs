using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf.Models;
using BusinessLogic.Controllers;
using DbAccesEf;
using System.Windows.Input;
using PresentationLayer.Commands;

namespace PresentationLayer.ViewModels
{
    public class SchablonExpenseViewModel : BaseViewModel
    {
        private AccountController accountController;

        private List<double> oldValues;
        private List<Account> _accounts;

        private double _total;
        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged(null);
            }
        }
        public List<Account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
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

        private void Save()
        {
            //Check for changes
            for(int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts.ElementAt(i).SchablonExpense != oldValues.ElementAt(i))
                {
                    accountController.ChangeSchablonExpense(Accounts.ElementAt(i).AccountId, Accounts.ElementAt(i).SchablonExpense);
                }
            }
            UpdateTotal();
        }

        public SchablonExpenseViewModel()
        {
            accountController = new AccountController(new MyContext());
            Accounts = accountController.GetAll().ToList();
            oldValues = new List<double>();

            //Clone list
            foreach(Account a in Accounts)
            {
                oldValues.Add(a.SchablonExpense);
            }

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            foreach(Account a in Accounts)
            {
                Total += a.SchablonExpense;
            }
        }
    }
}
