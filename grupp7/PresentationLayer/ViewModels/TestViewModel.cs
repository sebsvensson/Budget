using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresentationLayer.Commands;
using PresentationLayer.ViewModels;
using BusinessLogic.Controllers;
using DbAccesEf;

namespace PresentationLayer.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        private PersonellController personellController;
        private ProductController productController;

        private string _test;
        public string Test
        {
            get { return _test; }
            set
            {
                _test = value;
                OnPropertyChanged(null);
            }
        }
        public TestViewModel()
        {
            MyContext context = new MyContext();
            personellController = new PersonellController(context);
            productController = new ProductController(context);

        }
    }
}
