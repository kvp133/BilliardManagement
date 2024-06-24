using BilliardManagement.Models;
using BilliardManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
        public ICommand LoadedHome { get; set; }
        public ICommand CalculateCommand { get; set; }
        public ICommand TurnOnCommand { get; set; }
        private ObservableCollection<Dashboard> _Dashboard;
        public ObservableCollection<Dashboard> Dashboard { get => _Dashboard; set { _Dashboard = value; OnPropertyChanged(); } }

        // mọi thứ xử lý sẽ nằm trong này
        public MainViewModel()
        {
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
                    loadDashboard();
                CalculateCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    Isloaded = true;

                    // Find the current MainWindow instance and close it
                    var currentMainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    if (currentMainWindow != null)
                    {
                        currentMainWindow.Close();
                    }

                    // Execute the command
                    ExecuteCalculateCommand(p);

                    // Create and show a new instance of MainWindow
                    MainWindow newMainWindow = new MainWindow();
                    newMainWindow.Show();
                });

           
                    TurnOnCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                    {
                        Isloaded = true;
                        MainWindow wd = new MainWindow();
                        wd.ShowDialog();
                    }
              );
                }
                else
                    p.Close();
            }
              );
            LoadedEmployee = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
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
                    .Where(x => x.TableId == item.TableId && x.Table.Status == "Occupied")
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
                    totalPrice += (int)(time.TotalMinutes *(int) item.HourlyRate) / 60;
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
        }
        private void ExecuteCalculateCommand(object parameter)
        {
            if (parameter is Dashboard item)
            {
                //Update total price
                DateTime currentTime = DateTime.Now;
                var booking = DataProvider.Instance.DB.Bookings
                                .FirstOrDefault(x => x.TableId == item.Table.TableId && x.EndTime == null);
                item.Table.Status = "Available";

                if (booking != null)
                {
                    booking.SetEndTime(currentTime);
                    DataProvider.Instance.DB.Bookings.Update(booking);
                    DataProvider.Instance.DB.Tables.Update(item.Table);
                    DataProvider.Instance.DB.SaveChanges();
                }
            }
        }


        private void ExecuteTurnOnCommand(object parameter)
        {
            if (parameter is Dashboard item)
            {
                item.Table.Status = "Occupied";
                item.totalPrice = 0; // Example: Reset total price or set as needed
                MessageBox.Show($"Table {item.Table.TableNumber} status changed to Occupied");
            }
        }


    }
}

