using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class AuthModel
    {
        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; } = null!;

        [EmailAddress, Required]
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;

        public string? Address { get; set; }
        public string? ContactPerson { get; set; }

        public string? CoverageArea { get; set; }
    }
}
