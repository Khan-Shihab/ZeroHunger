using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class LoginResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
