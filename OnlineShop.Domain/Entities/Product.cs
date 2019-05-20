using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
        public ICollection<ProductCharacteristics> Characteristics { get; set; }
        public ICollection<ShoppingCartItem> CartItems { get; set; }
    }
}
