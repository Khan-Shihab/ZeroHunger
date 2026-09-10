using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class RestaurantUpdateModel
    {
        [Required, MaxLength(255)]
        public string Address { get; set; } = null!;

        [Required, MaxLength(120)]
        public string ContactPerson { get; set; } = null!;

        [Required]
        public bool IsVerified { get; set; }
    }
}
