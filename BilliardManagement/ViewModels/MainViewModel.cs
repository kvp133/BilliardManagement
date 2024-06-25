using BilliardManagement.Models;
using BilliardManagement.Properties;
using BilliardManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace BilliardManagement.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadedEmployee { get; set; }
        public ICommand LoadedProduct { get; set; }
        public ICommand LoadedTable { get; set; }
        public ICommand LoadedOrders { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand LoadedHome { get; set; }
        public ICommand CalculateCommand { get; set; }
        public ICommand TurnOnCommand { get; set; }
        private int _totalRevenueToday { get;set; }
        public int TotalRevenueToday { get => _totalRevenueToday; set { _totalRevenueToday = value;OnPropertyChanged(); } }
        private int _totalTableAvailable { get; set; }
        public int TotalTableAvailable { get => _totalTableAvailable; set { _totalTableAvailable = value; OnPropertyChanged(); } }
        private int _totalTableOccupied { get; set; }
        public int TotalTableOccupied { get => _totalTableOccupied; set { _totalTableOccupied = value; OnPropertyChanged(); } }
        private string _selectedStatus { get; set; }
        public string SelectedStatus { get => _selectedStatus; set { _selectedStatus = value; OnPropertyChanged(); FilterTable(); } }
        private string _SearchTable { get; set; }
        public string SearchTable { get => _SearchTable; set { _SearchTable = value; OnPropertyChanged(); FilterTable(); } }
        private DispatcherTimer _timer;


        private ObservableCollection<Dashboard> _Dashboard;
        public ObservableCollection<Dashboard> Dashboard { get => _Dashboard; set { _Dashboard = value; OnPropertyChanged(); } }

        // mọi thứ xử lý sẽ nằm trong này
        public MainViewModel()
        {
            
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1) // Update every minute
            };
            _timer.Tick += (s, e) =>
            {
                ExecuteTotalRevenueToday();
                ExcuteTotalTableAvailable();
                ExcuteTotalTableOccupied();
                FilterTable();
            };
            _timer.Start();
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
               
                if (loginVM.IsLogin)
                {
                    p.Show();
                    FilterTable();
                    CalculateCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                    {
                        Isloaded = true;

                        // Lấy thông tin của item được truyền vào
                        if (p is Dashboard item)
                        {
                            ExecuteCalculateCommand(item);
                        }
                    });

                   //When user select status of table, filter table
                   SelectionChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                    {
                        Isloaded = true;
                        FilterTable();
                    }
                      );

                    
                    TurnOnCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                    {
                        Isloaded = true;
                        if(p is Dashboard item)
                        {
                            ExecuteTurnOnCommand(item);
                        }
                    }
              );
                }
                else
                    p.Close();
            }
              );
            LoadedEmployee = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //Close current window and open EmployeeWindow 
                Isloaded = true;
                ManagerEmployeeWindow wd = new ManagerEmployeeWindow();
                wd.ShowDialog();
            }
              );
            LoadedProduct = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                ManagerProductWindow wd = new ManagerProductWindow();
                wd.ShowDialog();
            }
              );
            LoadedTable = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                TableWindow wd = new TableWindow();
                wd.ShowDialog();
            }
              );
            LoadedOrders = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                OrderWindow wd = new OrderWindow();
                wd.ShowDialog();
            }
              );
            LoadedHome = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                MainWindow wd = new MainWindow();
                wd.ShowDialog();
            }
              );


        }
        void loadDashboard()
        {
            Dashboard = new ObservableCollection<Dashboard>();

            // Lấy danh sách các bảng
            var listTable = DataProvider.Instance.DB.Tables.ToList();
            foreach (var item in listTable)
            {
                // Lấy booking đầu tiên nếu có, nếu không thì null
                var booking = DataProvider.Instance.DB.Bookings
                    .Where(x => x.TableId == item.TableId && x.Table.Status == "Occupied" && x.EndTime == null)
                    .FirstOrDefault();

                if (booking != null)
                {
                    // Lấy danh sách chi tiết booking và tính tổng giá
                    int totalPrice = 0;
                    var bookingDetails = DataProvider.Instance.DB.BookingDetails
                        .Where(x => x.BookingId == booking.BookingId)
                        .ToList();  // Sử dụng ToList() để thực hiện truy vấn và lưu kết quả vào bộ nhớ

                    foreach (var item1 in bookingDetails)
                    {
                        totalPrice += (int)item1.UnitPrice;
                    }
                    //Calculate total price equal current time - start time multiply hourly rate of table and calculate exactly minues
                    DateTime currentTime = DateTime.Now;
                    TimeSpan time = currentTime - booking.StartTime;
                    totalPrice += (int)(time.TotalMinutes * (int)item.HourlyRate) / 60;
                    // Tạo đối tượng Table mới
                    Table table = new Table()
                    {
                        TableId = item.TableId,
                        TableNumber = item.TableNumber,
                        HourlyRate = item.HourlyRate,
                        Status = item.Status,
                        Bookings = item.Bookings
                    };
                    Dashboard.Add(new Dashboard() { Table = table, totalPrice = totalPrice });

                }
                else
                {
                    // Thêm bảng với tổng giá bằng 0 nếu không có booking
                    Table table = new Table()
                    {
                        TableId = item.TableId,
                        TableNumber = item.TableNumber,
                        HourlyRate = item.HourlyRate,
                        Status = item.Status,
                        Bookings = item.Bookings
                    };
                    Dashboard.Add(new Dashboard() { Table = table, totalPrice = 0 });
                }
                
            }
            ExecuteTotalRevenueToday();
            ExcuteTotalTableAvailable();
            ExcuteTotalTableOccupied();
        }
        private void ExecuteCalculateCommand(Dashboard item)
        {

            //Save End Time and total Price to database
            var booking = DataProvider.Instance.DB.Bookings
                .Where(x => x.TableId == item.Table.TableId && x.Table.Status == "Occupied" && x.EndTime == null)
                .FirstOrDefault();
            booking.EndTime = DateTime.Now;
            booking.TotalAmount = item.totalPrice;
            DataProvider.Instance.DB.SaveChanges();
            //Change status of table to Available and save to database
            item.Table.Status = "Available";
            Table TableItem = DataProvider.Instance.DB.Tables.FirstOrDefault(x => x.TableId == booking.TableId);
            if (TableItem != null) TableItem.Status = "Available";
            DataProvider.Instance.DB.SaveChanges();
            loadDashboard();

        }


        private void ExecuteTurnOnCommand(Dashboard item)
        {

            item.Table.Status = "Occupied";
            Table table = DataProvider.Instance.DB.Tables.FirstOrDefault(x => x.TableId == item.Table.TableId && x.Status=="Available");
            if (table != null) table.Status = "Occupied";
            var booking = new Booking()
            {
                TableId = item.Table.TableId,
                StartTime = DateTime.Now,
                EndTime = null,
                TotalAmount = 0,
                EmployeeId = User.Default.Id
            };
            DataProvider.Instance.DB.Bookings.Add(booking);
            DataProvider.Instance.DB.SaveChanges();
            loadDashboard();

        }
        public void ExecuteTotalRevenueToday()
        {
             TotalRevenueToday = 0;
            //Count total amount of all booking today
            var listBooking = DataProvider.Instance.DB.Bookings.ToList();
            foreach (var item in listBooking)
            {
                if (item.StartTime.Date == DateTime.Now.Date)
                {
                    TotalRevenueToday += (int)item.TotalAmount;
                }
            }
            //update bliding 

            
        }
        public void ExcuteTotalTableAvailable()
        {
            int totalTable = 0;
            var listTable = DataProvider.Instance.DB.Tables.ToList();
            foreach (var item in listTable)
            {
                if (item.Status == "Available")
                {
                    totalTable++;
                }
            }
            TotalTableAvailable = totalTable;
        }
        public void ExcuteTotalTableOccupied()
        {
            int totalTable = 0;
            var listTable = DataProvider.Instance.DB.Tables.ToList();
            foreach (var item in listTable)
            {
                if (item.Status == "Occupied")
                {
                    totalTable++;
                }
            }
            TotalTableOccupied = totalTable;
        }
        public void FilterTable()
        {
            //split string SelectedStatus just All, Available,  to filter table
            
         string change = SelectedStatus.Contains("All") ? "All" : SelectedStatus.Contains("Available") ? "Available" : SelectedStatus.Contains("Occupied") ? "Occupied" : "Maintenance";

            Dashboard = new ObservableCollection<Dashboard>();
            if (change == "All")
            {
                loadDashboard();
            }
            else
            {
               
                var listTable = DataProvider.Instance.DB.Tables.Where(x => x.Status == change).ToList();
                foreach (var item in listTable)
                {
                    var booking = DataProvider.Instance.DB.Bookings
                        .Where(x => x.TableId == item.TableId && x.Table.Status == "Occupied" && x.EndTime == null)
                        .FirstOrDefault();

                    if (booking != null)
                    {
                        int totalPrice = 0;
                        var bookingDetails = DataProvider.Instance.DB.BookingDetails
                            .Where(x => x.BookingId == booking.BookingId)
                            .ToList();

                        foreach (var item1 in bookingDetails)
                        {
                            totalPrice += (int)item1.UnitPrice;
                        }
                        DateTime currentTime = DateTime.Now;
                        TimeSpan time = currentTime - booking.StartTime;
                        totalPrice += (int)(time.TotalMinutes * (int)item.HourlyRate) / 60;

                        Table table = new Table()
                        {
                            TableId = item.TableId,
                            TableNumber = item.TableNumber,
                            HourlyRate = item.HourlyRate,
                            Status = item.Status,
                            Bookings = item.Bookings
                        };
                        Dashboard.Add(new Dashboard() { Table = table, totalPrice = totalPrice });

                    }
                    else
                    {
                        Table table = new Table()
                        {
                            TableId = item.TableId,
                            TableNumber = item.TableNumber,
                            HourlyRate = item.HourlyRate,
                            Status = item.Status,
                            Bookings = item.Bookings
                        };
                        Dashboard.Add(new Dashboard() { Table = table, totalPrice = 0 });
                    }
                }
            }
            //Search table by table number
            if (SearchTable != null)
            {
                Dashboard = new ObservableCollection<Dashboard>(Dashboard.Where(x => x.Table.TableNumber.ToString().Contains(SearchTable)));
            }
           
        }
       


    }
}

