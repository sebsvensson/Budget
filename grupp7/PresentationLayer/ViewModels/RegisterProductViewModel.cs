using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PresentationLayer.Commands;
using BusinessLogic.Controllers;
using System.Collections.ObjectModel;
using DbAccesEf.Models;

namespace PresentationLayer.ViewModels
{
    public class RegisterProductViewModel : BaseViewModel
    {
        //Textbox bindings
        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                OnPropertyChanged(null);
                _productName = value;
            }
        }

        private string _xxxx;
        public string Xxxx
        {
            get { return _xxxx; }
            set
            {
                OnPropertyChanged(null);
                _xxxx = value;
            }
        }

        private string _productGroup;
        public string ProductGroup
        {
            get { return _productGroup; }
            set
            {
                OnPropertyChanged(null);
                _productGroup = value;
                ProductID = Xxxx + value.Substring(0, 1);


                /*if (Xxxx != null && Xxxx.Length == 4)
                {
                    ProductID = Xxxx + value.Substring(0, 1);
                }
                else
                {
                    ProductID = "Ange giltigt xxxx";
                }*/
            }
        }

        private string _productID;
        public string ProductID
        {
            get { return _productID; }
            set
            {
                OnPropertyChanged(null);
                _productID = value;
            }
        }



        private ObservableCollection<string> _productGroups;
        public ObservableCollection<string> ProductGroups
        {
            get { return _productGroups; }
            set
            {
                OnPropertyChanged(null);
                _productGroups = value;
            }
        }

        private ObservableCollection<string> _productCategories;
        public ObservableCollection<string> ProductCategories
        {
            get { return _productCategories; }
            set
            {
                OnPropertyChanged(null);
                _productCategories = value;
            }
        }

        private string _newProductGroup;
        public string NewProductGroup
        {
            get { return _newProductGroup; }
            set
            {
                OnPropertyChanged(null);
                _newProductGroup = value;
            }
        }

        private string _newProductCategory;
        public string NewProductCategory
        {
            get { return _newProductCategory; }
            set
            {
                OnPropertyChanged(null);
                _newProductCategory = value;
            }
        }


        private ProductController productController;
        private ICommand _registerProductCommand;
        public ICommand RegisterProductCommand
        {
            get
            {
                return _registerProductCommand ?? (_registerProductCommand = new CommandHandler(() => RegisterProduct()));
            }
        }

        private ICommand _addProductGroupCommand;
        public ICommand AddProductGroupCommand
        {
            get
            {
                return _addProductGroupCommand ?? (_addProductGroupCommand = new CommandHandler(() => AddProductGroup()));
            }
        }

        private ICommand _addProductCategoryCommand;
        public ICommand AddProductCategoryCommand
        {
            get
            {
                return _addProductCategoryCommand ?? (_addProductCategoryCommand = new CommandHandler(() => AddProductCategory()));
            }
        }

        public RegisterProductViewModel()
        {
            productController = new ProductController(new DbAccesEf.MyContext());

            ProductGroups = new ObservableCollection<string>();
            ProductCategories = new ObservableCollection<string>();
            foreach (var productGroup in productController.GetAllProductGroups())
            {
                ProductGroups.Add(productGroup.Name);
            }

            foreach (var productCategory in productController.GetAllProductCategories())
            {
                ProductCategories.Add(productCategory.Name);
            }
        }

        private void RegisterProduct()
        {

            //productController.RegisterProduct();
        }

        private void AddProductGroup()
        {
            //Check if already exists

            productController.AddProductGroup(NewProductGroup);
            ProductGroups.Add(NewProductGroup);
        }

        private void AddProductCategory()
        {
            //Check if already exists

            productController.AddProductCategory(NewProductCategory);
            ProductCategories.Add(NewProductCategory);
        }
    }
}
