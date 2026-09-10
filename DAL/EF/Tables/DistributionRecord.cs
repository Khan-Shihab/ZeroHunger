using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class DistributionRecord
{
    public int Id { get; set; }

    public int CollectRequestId { get; set; }

    public string DistributionPoint { get; set; } = null!;

    public int BeneficiaryCount { get; set; }

    public DateTime DistributedAt { get; set; }

    public string? Notes { get; set; }

    public virtual CollectRequest CollectRequest { get; set; } = null!;
}
