using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Entities
{
    public class ProductCharacteristics : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
