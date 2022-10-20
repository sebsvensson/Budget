using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccesEf.Models;
using BusinessLogic.Controllers;
using DbAccesEf;

namespace PresentationLayer.ViewModels
{
    public class ResourceAllocationViewModel : BaseViewModel
    {
        private PersonellController personellController;

        private List<Personell> _personells;
        public List<Personell> Personells
        {
            get { return _personells; }
            set
            {
                _personells = value;
                OnPropertyChanged(null);
            }
        }

        public ResourceAllocationViewModel()
        {
            personellController = new PersonellController(new MyContext());
            Personells = personellController.GetAll().ToList();
        }
    }
}
