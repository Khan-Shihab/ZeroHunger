using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class AcceptModel
    {
        [Required]
        public int EmployeeId { get; set; }
    }
}
