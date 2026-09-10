using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? CoverageArea { get; set; }

        public string Availability { get; set; } = null!;

    }
}
