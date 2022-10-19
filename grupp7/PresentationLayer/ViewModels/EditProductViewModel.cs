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
                ProductID = Xxxx + value.Substring(0, 2);
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
                if (ProductGroup != null)
                {
                    ProductID = value + ProductGroup.Substring(0, 2);
                }
                else
                {
                    ProductID = value;
                }
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
    }
}

