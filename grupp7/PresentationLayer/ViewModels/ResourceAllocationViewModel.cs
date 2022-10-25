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
    public class ResourceAllocationViewModel : BaseViewModel
    {
        private PersonellController personellController;
        private ProductController productController;

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

        private ICommand _testCommand;
        public ICommand TestCommand
        {
            get
            {
                return _testCommand ?? (_testCommand = new CommandHandler(() => Test()));
            }
        }

        public void Test()
        {
            //System.Diagnostics.Debug.WriteLine(Personells.ElementAt(0).AllocationInput);
        }

        public ResourceAllocationViewModel()
        {
            MyContext context = new MyContext();
            personellController = new PersonellController(context);
            productController = new ProductController(context);

            Personells = personellController.GetAll().ToList();
        }
    }
}
