using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresentationLayer.Commands;
using PresentationLayer.ViewModels;


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

        private ICommand _troll;
        public ICommand Troll
        {
            get
            {
                return _troll ?? (_troll = new CommandHandler(() => TrollButton()));
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
        }

        private string _trollMargin = "0,10,0,0";
        public string TrollMargin
        {
            get { return _trollMargin; }
            set
            {
                OnPropertyChanged(null);
                _trollMargin = value;
            }
        }

        private int counter = 0;
        private void TrollButton()
        {
            counter++;
            switch (counter)
            {
                case 1:
                    TrollMargin = "200,10,0,0";
                    break;
                case 2:
                    TrollMargin = "0,10,200,0";
                    break;
                case 3:
                    TrollMargin = "0,10,0,0";
                    counter = 0;
                    break;
            }
        }
    }
}
