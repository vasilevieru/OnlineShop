using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Users.Models
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
