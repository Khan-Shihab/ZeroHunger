using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class StatusLogCreateModel
    {
        public int CollectRequestId { get; set; }

        public string? OldStatus { get; set; }

        [Required]
        public string NewStatus { get; set; } = null!;

        public int ChangedBy { get; set; }

        public string? Note { get; set; }
    }
}
