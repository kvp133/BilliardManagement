using System;
using System.Collections.Generic;

namespace BilliardManagement.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int TableId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Employee Employee { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;

    internal void SetEndTime(DateTime currentTime)
    {
        this.EndTime = currentTime;
    }
}
