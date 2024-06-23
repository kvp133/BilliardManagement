using BilliardManagement.Models;
using BilliardManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BilliardManagement.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set;}
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        private string _userName;
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(); } }
        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
     

        // mọi thứ xử lý sẽ nằm trong này
        public LoginViewModel()
        {
           
            IsLogin = false;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Login(p);
            }
              );
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            }
              );


        }
        void Login(Window p)
        {
           if(p==null) return;
           var accountCount = DataProvider.Instance.DB.Employees.Where(x => x.Username == UserName && x.Password == Password).Count();
            if(accountCount == 0)
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
                return;
            }
            if(accountCount == 1) {
                MessageBox.Show("Đăng nhập thành công");
                IsLogin = true;
                p.Close();

            }
           
        }
    }
}
