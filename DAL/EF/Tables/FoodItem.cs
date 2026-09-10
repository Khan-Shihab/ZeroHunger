using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class FoodItem
{
    public int Id { get; set; }

    public int CollectRequestId { get; set; }

    public string Name { get; set; } = null!;

    public double Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public string Category { get; set; } = null!;

    public virtual CollectRequest CollectRequest { get; set; } = null!;
}
