using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Core.Domain
{
    public class ShoppingCartItem : BaseEntity
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
