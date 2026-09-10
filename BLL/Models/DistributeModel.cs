using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class DistributeModel
    {
        [Required, MaxLength(255)]
        public string DistributionPoint { get; set; } = null!;

        [Required]
        public int BeneficiaryCount { get; set; }

        public string? Notes { get; set; }
    }
}
