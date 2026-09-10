using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class UserUpdateModel
    {
        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }

        [Required]
        public string Role { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }
    }
}
