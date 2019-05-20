
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool Active => DateTime.UtcNow <= Expires;
    }
}
