using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class RestaurantModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string ContactPerson { get; set; } = null!;

        public bool IsVerified { get; set; }
    }
}
