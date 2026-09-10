using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class StatusLog
{
    public int Id { get; set; }

    public int CollectRequestId { get; set; }

    public string? OldStatus { get; set; }

    public string NewStatus { get; set; } = null!;

    public int ChangedBy { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User ChangedByNavigation { get; set; } = null!;

    public virtual CollectRequest CollectRequest { get; set; } = null!;
}
