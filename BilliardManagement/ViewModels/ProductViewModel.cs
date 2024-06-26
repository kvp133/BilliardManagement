using BilliardManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BilliardManagement.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {

        public ICommand AddCommand { get; set; }      
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private ObservableCollection<Product> _List;
        public ObservableCollection<Product> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        
        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }
        private decimal _Price;
        public decimal Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }
        private string _Description;
        public string Description { get => _Description; set { _Description = value; OnPropertyChanged(); } }
        private string _SearchText;
        public string SearchText { get => _SearchText; set { _SearchText = value; OnPropertyChanged(); } }
        private Product _SelectedItem;
        public Product SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                if (SelectedItem != null)
                {
                    ProductName = SelectedItem.ProductName;
                    Price = SelectedItem.Price;
                    Description = SelectedItem.Description;
                }
                OnPropertyChanged();
            }
        }





        // mọi thứ xử lý sẽ nằm trong này
        public ProductViewModel()
        {
            List = new ObservableCollection<Product>(DataProvider.Instance.DB.Products);
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(ProductName) || string.IsNullOrEmpty(Price.ToString()))
                    return false;
                if (DataProvider.Instance.DB.Products.Where(x => x.ProductName == ProductName).Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var product = new Product() { ProductName = ProductName, Price = Price, Description = Description };
                DataProvider.Instance.DB.Products.Add(product);
                DataProvider.Instance.DB.SaveChanges();
                List.Add(product);
                new OrderViewModel().loadListTable();
            }
              );
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(ProductName) || string.IsNullOrEmpty(Price.ToString()) || SelectedItem == null)
                    return false;
                if (DataProvider.Instance.DB.Products.Where(x => x.ProductName == ProductName && x.ProductId != SelectedItem.ProductId).Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var product = DataProvider.Instance.DB.Products.Where(x => x.ProductId == SelectedItem.ProductId).FirstOrDefault();
                product.ProductName = ProductName;
                product.Price = Price;
                product.Description = Description;
                DataProvider.Instance.DB.SaveChanges();
                SelectedItem.ProductName = ProductName;
                SelectedItem.Price = Price;
                SelectedItem.Description = Description;
                List = new ObservableCollection<Product>(DataProvider.Instance.DB.Products);
                
                new OrderViewModel().loadListTable();

            }
              );
            DeleteCommand = new RelayCommand<object>((p) =>
            {
   return SelectedItem != null; 
            }, (p) =>
            {
                var product = DataProvider.Instance.DB.Products.Where(x => x.ProductId == SelectedItem.ProductId).FirstOrDefault();
                DataProvider.Instance.DB.BookingDetails.RemoveRange(product.BookingDetails);
                DataProvider.Instance.DB.Products.Remove(product);
                DataProvider.Instance.DB.SaveChanges();
                List.Remove(product);
                OrderViewModel orderViewModel = new OrderViewModel();
                new OrderViewModel().loadListTable();
            }
              );

           

        }
    }
}
