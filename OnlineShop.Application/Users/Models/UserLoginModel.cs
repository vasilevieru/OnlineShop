using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Users.Models
{
    public class UserLoginModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
