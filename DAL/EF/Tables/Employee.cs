using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Employee
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Phone { get; set; } = null!;

    public string? CoverageArea { get; set; }

    public string Availability { get; set; } = null!;

    public virtual ICollection<CollectRequest> CollectRequests { get; set; } = new List<CollectRequest>();

    public virtual User User { get; set; } = null!;
}
