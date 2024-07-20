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
    public class EmployeeViewModel : BaseViewModel
    {
       
        public ICommand AddCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private ObservableCollection<Employee> _List;
        public ObservableCollection<Employee> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private string _username;
       public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        private string _email;
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        private string _fullname;
        public string FullName { get => _fullname; set { _fullname = value; OnPropertyChanged(); } }
        private string _phonenumber;
        public string PhoneNumber { get => _phonenumber; set { _phonenumber = value; OnPropertyChanged(); } }
        private string _role;
        public string Role { get => _role; set { _role = value; OnPropertyChanged(); } }
        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string _TesitngRole;
        public string TestingRole { get => _TesitngRole; set { _TesitngRole = value; OnPropertyChanged(); } }
        private string _SearchTable { get; set; }
        public string SearchTable { get => _SearchTable; set { _SearchTable = value; OnPropertyChanged(); FilterTable(); } }
        private ObservableCollection<string> _roles;
        public ObservableCollection<string> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }
        private Employee _SelectedItem;
        public Employee SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                UpdateComboBoxItems();
                if (SelectedItem != null)
                {
                    Username = SelectedItem.Username;
                    Password = SelectedItem.Password;
                    FullName = SelectedItem.FullName;
                    Email = SelectedItem.Email;
                    PhoneNumber = SelectedItem.PhoneNumber;
                    Role = SelectedItem.Role;
                    TestingRole = SelectedItem.Role;
                }
                OnPropertyChanged();
            }
        }
        private void UpdateComboBoxItems()
        {

               
            if (SelectedItem != null)
            {
                // Optionally set TestingRole if it depends on SelectedRecord
                TestingRole = SelectedItem.Role;
            }
        }

        // mọi thứ xử lý sẽ nằm trong này
        public EmployeeViewModel()
        {
            Roles = new ObservableCollection<string>();
            Roles.Add("Manager");
            Roles.Add("Staff");
            List = new ObservableCollection<Employee>(DataProvider.Instance.DB.Employees.ToList());
            AddCommand = new RelayCommand<object>((p) => { 
                if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Role))
                {
                    return false;
                }
                if(DataProvider.Instance.DB.Employees.Where(x => x.Username == Username || x.Email == Email).Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                //if Role contains "Staff" or "Mana ger" then set Role to "Staff" or "Manager"
                if(Role.Contains("Staff")) Role = "Staff";
                if(Role.Contains("Manager")) Role = "Manager";
                var employee = new Employee() { Username = Username, Password = Password, FullName = FullName, Email = Email, PhoneNumber = PhoneNumber, Role = Role };
                DataProvider.Instance.DB.Employees.Add(employee);
                DataProvider.Instance.DB.SaveChanges();
                List.Add(employee);
               
            }
              );
            DeleteCommand = new RelayCommand<object>((p) => { return SelectedItem != null; }, (p) =>
            {
                Employee employee = DataProvider.Instance.DB.Employees.Where(x => x.EmployeeId == SelectedItem.EmployeeId).FirstOrDefault();
                DataProvider.Instance.DB.BookingDetails.RemoveRange(employee.Bookings.SelectMany(x => x.BookingDetails));
                DataProvider.Instance.DB.Bookings.RemoveRange(employee.Bookings);
                DataProvider.Instance.DB.Employees.Remove(employee);
                DataProvider.Instance.DB.SaveChanges();
                List.Remove(employee);
            });
            EditCommand = new RelayCommand<object>((p) => { 
                if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Role))
                {
                    return false;
                }
                //if username or email equal selected item return true and allow to edit username or email already exist in database return false
                if (DataProvider.Instance.DB.Employees.Where(x => x.Username == Username || x.Email == Email).Count() != 0)
                {
                    if (DataProvider.Instance.DB.Employees.Where(x => x.Username == Username || x.Email == Email).FirstOrDefault().Username == SelectedItem.Username || DataProvider.Instance.DB.Employees.Where(x => x.Username == Username || x.Email == Email).FirstOrDefault().Email == SelectedItem.Email)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }, (p) =>
            {
                if (Role.Contains("Staff")) Role = "Staff";
                if (Role.Contains("Manager")) Role = "Manager";
                Employee employee = DataProvider.Instance.DB.Employees.Where(x => x.EmployeeId == SelectedItem.EmployeeId).FirstOrDefault();
                employee.Username = Username;
                employee.Password = Password;
                employee.FullName = FullName;
                employee.Email = Email;
                employee.PhoneNumber = PhoneNumber;
                employee.Role = Role;
                DataProvider.Instance.DB.SaveChanges();
                List = new ObservableCollection<Employee>(DataProvider.Instance.DB.Employees.ToList());
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            }
             );


        }
        public void FilterTable()
        {
            if (SearchTable != null)
            {
                List = new ObservableCollection<Employee>(DataProvider.Instance.DB.Employees.Where(x => x.FullName.ToString().Contains(SearchTable)));
            }
        }
    }
   
}
