using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Core.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<ShoppingCartItem> CartItems { get; set; }
    }
}
