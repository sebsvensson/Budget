using BusinessLogic.Controllers;
using DbAccesEf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                GetProductInfo(SelectedCustomID); // metod för att hämta efter valt id 
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

        private void GetProductInfo(string selectedCustomID)
        {
            System.Diagnostics.Debug.WriteLine(selectedCustomID);             //Hittar ej
            DbAccesEf.Models.Product product = productController.GetByID(selectedCustomID);
            ProductName = product.ProductName;


        }
    }
}

