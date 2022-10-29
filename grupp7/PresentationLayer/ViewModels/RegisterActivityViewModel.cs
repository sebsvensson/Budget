using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Controllers;
using System.Windows.Input;
using PresentationLayer.Commands;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class RegisterActivityViewModel : BaseViewModel
    {
        private ActivityController activityController;
        private DbAccesEf.MyContext context;

        //Bindings
        private string _activityName;
        public string ActivityName
        {
            get { return _activityName; }
            set
            {
                _activityName = value;
                OnPropertyChanged(null);
            }
        }
        private string _activityXxxx;
        public string ActivityXxxx
        {
            get { return _activityXxxx; }
            set
            {
                _activityXxxx = value;
                OnPropertyChanged(null);
            }
        }

        private string _selectedAFFODepartment;
        public string SelectedAFFODepartment
        {
            get { return _selectedAFFODepartment; }
            set
            {
                CustomID = ActivityXxxx + value;
                _selectedAFFODepartment = value;
                OnPropertyChanged(null);
            }
        }

        private List<string> _aFFODepartment;
        public List<string> AFFODepartment
        {
            get { return _aFFODepartment; }
            set
            {
                _aFFODepartment = value;
                OnPropertyChanged(null);
            }
        }

        public RegisterActivityViewModel()
        {
            AFFODepartment = new List<string>()
            {
                "FO",
                "AO"

            };

            context = new DbAccesEf.MyContext();
            activityController = new ActivityController(context);
        }

        private string _customID;
        public string CustomID
        {
            get { return _customID; }
            set
            {
                _customID = value;
                OnPropertyChanged(null);
            }
        }

        private ICommand _registerActivityCommand;
        public ICommand RegisterActivityCommand
        {
            get
            {
                return _registerActivityCommand ?? (_registerActivityCommand = new CommandHandler(() => RegisterActivity()));
            }
        }

        private void RegisterActivity()
        {
            if (ActivityXxxx.Length == 3 && ActivityName != null && SelectedAFFODepartment != null && CustomID != null)
            {
                try
                {
                    activityController.RegisterActivity(ActivityName, ActivityXxxx, SelectedAFFODepartment, CustomID);

                }
                catch (Exception e)
                {
                    MessageBox.Show("A handled exception just occurred: " + e.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Fyll i alla uppgifter");
            }

        }
    }
}
