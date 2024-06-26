using BilliardManagement.ViewModels;
using System;
using System.Collections.Generic;

namespace BilliardManagement.Models;

public partial class Table:BaseViewModel
{
    private int _tableId;
    public int TableId { get => _tableId; set { _tableId = value; OnPropertyChanged(); } }

    private int _tableNumber;
    public int TableNumber { get => _tableNumber; set { _tableNumber = value; OnPropertyChanged(); } }
    
    private decimal _hourlyRate;
    public decimal HourlyRate { get => _hourlyRate; set { _hourlyRate = value; OnPropertyChanged(); } }

    private string _status;
    public string Status { get => _status; set { _status = value; OnPropertyChanged(); } }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
