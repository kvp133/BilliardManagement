using BilliardManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BilliardManagement.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private ObservableCollection<BookingDetail> _List;
        public ObservableCollection<BookingDetail> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private string _TableNumber;
        public string TableNumber { get => _TableNumber; set { _TableNumber = value; OnPropertyChanged(); } }
        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }
        private decimal _Price;
        public decimal Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }
        public int _Quantity { get; set; }
        public int Quantity {  get => _Quantity; set{ _Quantity = value; OnPropertyChanged(); } }
        private ObservableCollection<Table> _ListTable;
        public ObservableCollection<Table> ListTable { get => _ListTable; set { _ListTable = value;OnPropertyChanged(); } }
        private ObservableCollection<Product> _ListProduct;
        public ObservableCollection<Product> ListProduct { get => _ListProduct; set { _ListProduct = value; OnPropertyChanged(); } }
        private Table _SelectedTable;
        public Table SelectedTable { get => _SelectedTable; set { _SelectedTable = value;OnPropertyChanged(); } }
        private Product _SelectedProduct;
        public Product SelectedProduct { get => _SelectedProduct; set { _SelectedProduct = value; OnPropertyChanged(); } }
        private BookingDetail _SelectedItem;
        public BookingDetail SelectedItem { get => _SelectedItem; set { _SelectedItem = value;
                if(SelectedItem != null)
                {
                    SelectedTable = SelectedItem.Booking.Table;
                    SelectedProduct = SelectedItem.Product;
                    Quantity = SelectedItem.Quantity;
                    TableNumber = SelectedItem.Booking.Table.TableNumber.ToString();
                    ProductName = SelectedItem.Product.ProductName;
                    Price = SelectedItem.Product.Price;
                }
                OnPropertyChanged(); } }
        public OrderViewModel() {
            List = new ObservableCollection<BookingDetail>(DataProvider.Instance.DB.BookingDetails);
            ListTable= new ObservableCollection<Table>(DataProvider.Instance.DB.Tables.Where(x => x.Status == "Available"));
            ListProduct = new ObservableCollection<Product>(DataProvider.Instance.DB.Products);
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedTable == null || SelectedProduct == null)
                    return false;
                if(SelectedTable.Status=="Available")
                    return false;
                return true;
            }, (p) =>
            {
                BookingDetail bookingDetail = new BookingDetail();
                bookingDetail.Product = SelectedProduct;
                bookingDetail.UnitPrice = SelectedProduct.Price;
                bookingDetail.Booking = DataProvider.Instance.DB.Bookings.Where(x => x.EndTime == null && x.Table == SelectedTable).FirstOrDefault();
                bookingDetail.Quantity = Quantity;
                bookingDetail.BookingId = bookingDetail.Booking.BookingId;
                bookingDetail.ProductId = bookingDetail.Product.ProductId;
                DataProvider.Instance.DB.Add(bookingDetail);
                DataProvider.Instance.DB.SaveChanges();
                List.Add(bookingDetail);
            });

        }


    }
}
