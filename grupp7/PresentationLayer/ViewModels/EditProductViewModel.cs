using BusinessLogic.Controllers;
using DbAccesEf.Models;
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
    public class EditProductViewModel : BaseViewModel
    {
        private ProductController productController;
        private DbAccesEf.MyContext context;

        public EditProductViewModel()
        {
            context = new DbAccesEf.MyContext();
            productController = new ProductController(context);

            ProductGroups = new ObservableCollection<string>();
            ProductCategories = new ObservableCollection<string>();
            CustomIDs = new ObservableCollection<string>();
            ProductDepartments = new ObservableCollection<string>()
            {
                "Drift",
                "Utv/Förv"
            };

            foreach (ProductGroup productGroup in productController.GetAllProductGroups())
            {
                ProductGroups.Add(productGroup.Name);
            }
            foreach (ProductCategory productCategory in productController.GetAllProductCategories())
            {
                ProductCategories.Add(productCategory.Name);
            }
            foreach (DbAccesEf.Models.Product product in productController.GetAllProducts())
            {
                CustomIDs.Add(product.CustomId);
            }
           
        }

        private string _customId;
        public string CustomId
        {
            get { return _customId; }
            set
            {
                OnPropertyChanged(null);
                _customId = value;
            }
        }
        private string _selectedCustomID;
        public string SelectedCustomID
        {
            get { return _selectedCustomID; }
            set
            {
                GetProductInfo(value);
                OnPropertyChanged(null);
                _selectedCustomID = value;
            }
        }
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
        private string _productGroup;
        public string ProductGroup
        {
            get { return _productGroup; }
            set
            {
                
                _productGroup = value;
                OnPropertyChanged(null);
            }
        }
        private string _productCategory;
        public string ProductCategory
        {
            get { return _productCategory; }
            set
            {
                _productCategory = value;
                OnPropertyChanged(null);
            }
        }
        private int _productID;
        public int ProductID
        {
            get { return _productID; }
            set
            {
                OnPropertyChanged(null);
                _productID = value;
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
        private ObservableCollection<string> _productDepartments;
        public ObservableCollection<string> ProductDepartments
        {
            get { return _productDepartments; }
            set
            {
                OnPropertyChanged(null);
                _productDepartments = value;
            }
        }
        private ICommand _editProductCommand;
        public ICommand EditProductCommand
        {
            get
            {
                return _editProductCommand ?? (_editProductCommand = new CommandHandler(() => EditProduct()));
            }
        }
        private void EditProduct()
        {
            if (Xxxx.Length == 4 && SelectedCustomID != null && ProductName != null && ProductCategory != null && ProductGroup != null)
            
            {
                try
                {
                    productController.EditProduct(SelectedCustomID, ProductName, Xxxx, ProductGroup, ProductCategory, SelectedDepartment);
                }
                catch (Exception e)
                {
                    MessageBox.Show("A handled exception just occurred: " + e.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else
            {
                MessageBox.Show("Fyll i i alla uppgifter");
            }
        }

         private void GetProductInfo(string selectedCustomID)
        {
            
            DbAccesEf.Models.Product product = productController.GetByID(selectedCustomID);
            ProductName = product.ProductName;
            Xxxx = product.Xxxx;
            ProductGroup = product.ProductGroup.Name;
            ProductCategory = product.ProductCategory.Name;
            SelectedDepartment = product.Department;
        }
    }
}

