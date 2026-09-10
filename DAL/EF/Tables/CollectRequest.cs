using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class CollectRequest
{
    public int Id { get; set; }

    public int RestaurantId { get; set; }

    public int? EmployeeId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime MaxPreserveUntil { get; set; }

    public string? PickupNotes { get; set; }

    public DateTime? AcceptedAt { get; set; }

    public DateTime? CollectedAt { get; set; }

    public DateTime? DistributedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual DistributionRecord? DistributionRecord { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<FoodItem> FoodItems { get; set; } = new List<FoodItem>();

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual ICollection<StatusLog> StatusLogs { get; set; } = new List<StatusLog>();
}
