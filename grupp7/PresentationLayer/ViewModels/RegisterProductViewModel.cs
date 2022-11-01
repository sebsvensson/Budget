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
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class RegisterProductViewModel : BaseViewModel
    {
        private ProductController productController;
        private DbAccesEf.MyContext context;
        //Textbox bindings
        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                OnPropertyChanged(null);
            }
        }

        private string _xxxx;
        public string Xxxx
        {
            get { return _xxxx; }
            set
            {
                if (SelectedProductGroup != null)
                {
                    ProductID = value + SelectedProductGroup.Substring(0, 2);
                }
                else
                {
                    ProductID = value;
                }

                OnPropertyChanged(null);
                _xxxx = value;
            }
        }

        private string _productGroup;
        public string SelectedProductGroup
        {
            get { return _productGroup; }
            set
            {
                ProductID = Xxxx + value.Substring(0, 2);
                _productGroup = value;
                OnPropertyChanged(null);
            }
        }

        private string _selectedProductCategory;
        public string SelectedProductCategory
        {
            get { return _selectedProductCategory; }
            set
            {
                _selectedProductCategory = value;
                OnPropertyChanged(null);
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

        private ObservableCollection<string> _departments;
        public ObservableCollection<string> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                OnPropertyChanged(null);
            }
        }

        private string _selectedDepartment;
        public string SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(null);
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
            context = new DbAccesEf.MyContext();
            productController = new ProductController(context);

            ProductGroups = new ObservableCollection<string>();
            ProductCategories = new ObservableCollection<string>();
            Departments = new ObservableCollection<string>()
            {
                "Utv/Förv",
                "Drift"
            };

            foreach (ProductGroup productGroup in productController.GetAllProductGroups())
            {
                ProductGroups.Add(productGroup.Name);
            }

            foreach (ProductCategory productCategory in productController.GetAllProductCategories())
            {
                //no duplicates
                if (!ProductCategories.Any(c => c == productCategory.Name))
                {
                    ProductCategories.Add(productCategory.Name);
                }
            }
        }

        private void RegisterProduct()
        {
            if (Xxxx.Length == 4 && SelectedProductCategory != null && SelectedProductGroup != null && ProductName != null)
            {
                try
                {
                    ProductCategory productCategory = productController.GetProductCategory(SelectedProductCategory);
                    ProductGroup productGroup = productController.GetProductGroup(SelectedProductGroup);
                    productController.RegisterProduct(ProductName, Xxxx, productCategory, productGroup, SelectedDepartment);
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
