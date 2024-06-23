using System;
using System.Collections.Generic;

namespace BilliardManagement.Models;

public partial class Table
{
    public int TableId { get; set; }

    public int TableNumber { get; set; }

    public decimal HourlyRate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
