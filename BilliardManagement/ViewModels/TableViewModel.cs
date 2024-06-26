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
    public class TableViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private ObservableCollection<Table> _List;
        public ObservableCollection<Table> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private string _TableNumber;
        public string TableNumber { get => _TableNumber; set { _TableNumber = value; OnPropertyChanged(); } }
        private string _HourlyRate;
        public string HourlyRate { get => _HourlyRate; set { _HourlyRate = value; OnPropertyChanged(); } }
        private string _Status;
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }
        private string _SearchText;
        public string SearchText { get => _SearchText; set { _SearchText = value; OnPropertyChanged(); } }
        private string _StatusSelected;
        public string StatusSelected { get => _StatusSelected; set { _StatusSelected = value; OnPropertyChanged(); } }
        private ObservableCollection<string> _ListStatus;
        public ObservableCollection<string> ListStatus
        {
            get => _ListStatus;
            set
            {
                _ListStatus = value;
                OnPropertyChanged();
            }
        }
        private Table _SelectedItem;
        public Table SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                if (SelectedItem != null)
                {
                    TableNumber = SelectedItem.TableNumber.ToString();
                    HourlyRate = SelectedItem.HourlyRate.ToString();
                    Status = SelectedItem.Status;
                    StatusSelected = SelectedItem.Status;
                }
                OnPropertyChanged();
            }
        }

        public TableViewModel() 
        {
            List = new ObservableCollection<Table>(DataProvider.Instance.DB.Tables);
            ListStatus = new ObservableCollection<string>() { "Available", "Occupied", "Maintenance" };
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (TableNumber == null || HourlyRate == null)
                    return false;
                if (DataProvider.Instance.DB.Tables.Where(x => x.TableNumber == int.Parse(TableNumber)).Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var table = new Table() { TableNumber = int.Parse(TableNumber), HourlyRate = decimal.Parse(HourlyRate) , Status = "Available" };
                DataProvider.Instance.DB.Tables.Add(table);
                DataProvider.Instance.DB.SaveChanges();
                List.Add(table);
                
            });
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (TableNumber == null || HourlyRate == null || SelectedItem == null)
                    return false;
                var displayList = DataProvider.Instance.DB.Tables.Where(x => x.TableNumber == int.Parse(TableNumber));
                if (displayList.Count() != 0 && displayList.First().TableId != SelectedItem.TableId)
                    return false;
                return true;
            }, (p) =>
            {
                var table = DataProvider.Instance.DB.Tables.Where(x => x.TableId == SelectedItem.TableId).SingleOrDefault();
                table.TableNumber = int.Parse(TableNumber);
                table.HourlyRate = Convert.ToDecimal(HourlyRate);
                DataProvider.Instance.DB.SaveChanges();
            SelectedItem.TableNumber = int.Parse(TableNumber);
                SelectedItem.HourlyRate = decimal.Parse(HourlyRate);
                List = new ObservableCollection<Table>(DataProvider.Instance.DB.Tables);
                OrderViewModel orderViewModel = new OrderViewModel();
                orderViewModel.loadListTable();
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                var table = DataProvider.Instance.DB.Tables.Where(x => x.TableId == SelectedItem.TableId).SingleOrDefault();
                DataProvider.Instance.DB.Bookings.RemoveRange(table.Bookings);
                DataProvider.Instance.DB.Tables.Remove(table);
                DataProvider.Instance.DB.SaveChanges();
                List.Remove(table);
                OrderViewModel orderViewModel = new OrderViewModel();
                orderViewModel.loadListTable();
            });
        
        }
    }
}
