using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class FoodItemCreateModel
    {
        [Required, MaxLength(120)]
        public string Name { get; set; } = null!;

        [Required]
        public double Quantity { get; set; }

        [Required]
        public string Unit { get; set; } = null!;  

        public string Category { get; set; } = "COOKED";
    }
}
