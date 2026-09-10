using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class EmployeeUpdateModel
    {
        [Required, MaxLength(20)]
        public string Phone { get; set; } = null!;
        public string? CoverageArea { get; set; }
        [Required]
        public string Availability { get; set; } = null!;
    }
}
