using BusinessLogic.Controllers;
using PresentationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.ViewModels
{
    public class EditActivityViewModel : BaseViewModel
    {
        private ActivityController activityController;
        private DbAccesEf.MyContext context;

        public EditActivityViewModel()
        {
            context = new DbAccesEf.MyContext();
            activityController = new ActivityController(context);
            CustomIDs = new ObservableCollection<string>();
            AFFODepartments = new ObservableCollection<string>()
            {
                "FO",
                "AO"
            };

            foreach (DbAccesEf.Models.Activity activity in activityController.GetAllActivities())
            {
                CustomIDs.Add(activity.CustomID);
            }
        }

        private ObservableCollection<string> _aFFODepartments;
        public ObservableCollection<string> AFFODepartments
        {
            get { return _aFFODepartments; }
            set
            {
                OnPropertyChanged(null);
                _aFFODepartments = value;
            }
        }

        private ObservableCollection<string> _customIDs;
        public ObservableCollection<string> CustomIDs
        {
            get { return _customIDs; }
            set
            {
                OnPropertyChanged(null);
                _customIDs = value;
            }
        }

        private string _customID;
        public string CustomID
        {
            get { return _customID; }
            set
            {
                OnPropertyChanged(null);
                _customID = value;
            }
        }

        private string _selectedCustomID;
        public string SelectedCustomID
        {
            get { return _selectedCustomID; }
            set
            {
                GetActivityInfo(value);
                OnPropertyChanged(null);
                _selectedCustomID = value;
            }
        }

        private string _activityXxxx;
        public string ActivityXxxx
        {
            get { return _activityXxxx; }
            set
            {
                OnPropertyChanged(null);
                _activityXxxx = value;
            }
        }

        private string _selectedActivityXxxx;
        public string SelectedActivityXxxx
        {
            get { return _selectedActivityXxxx; }
            set
            {
                OnPropertyChanged(null);
                _selectedActivityXxxx = value;
            }
        }

        private string _activityName;
        public string ActivityName
        {
            get { return _activityName; }
            set
            {
                OnPropertyChanged(null);
                _activityName = value;
            }
        }

        private string _selectedActivityName;
        public string SelectedActivityName
        {
            get { return _selectedActivityName; }
            set
            {
                OnPropertyChanged(null);
                _selectedActivityName = value;
            }
        }

        private string _aFFODepartment;
        public string AFFODepartment
        {
            get { return _aFFODepartment; }
            set
            {
                OnPropertyChanged(null);
                _aFFODepartment = value;
            }
        }

        private string _selectedAFFODepartment;
        public string SelectedAFFODepartment
        {
            get { return _selectedAFFODepartment; }
            set
            {
                OnPropertyChanged(null);
                _selectedAFFODepartment = value;
            }
        }

        private void GetActivityInfo(string selectedCustomID)
        {
            DbAccesEf.Models.Activity activity = activityController.GetByID(selectedCustomID);
            ActivityName = activity.ActivityName;
            ActivityXxxx = activity.ActivityXxxx;
            AFFODepartment = activity.AFFODepartment;
        }

        private ICommand _editActivityCommand;
        public ICommand EditActivityCommand
        {
            get
            {
                return _editActivityCommand ?? (_editActivityCommand = new CommandHandler(() => EditActivity()));
            }
        }
        private void EditActivity()
        {
            if (SelectedCustomID != null && ActivityName != null && ActivityXxxx.Length == 4 && AFFODepartment != null)
            {

                activityController.EditActivity(SelectedCustomID, ActivityName, ActivityXxxx, AFFODepartment);
            }
            else
            {
                MessageBox.Show("Fyll i alla uppgifter");
            }


        }
    }
}
