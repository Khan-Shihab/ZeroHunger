using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class CollectRequestCreateModel
    {
        public int RestaurantId { get; set; }
        [Required]
        public DateTime MaxPreserveUntil { get; set; }

        public string? PickupNotes { get; set; }

        [Required, MinLength(1)]
        public List<FoodItemCreateModel> FoodItems { get; set; } = new();
    }
}
