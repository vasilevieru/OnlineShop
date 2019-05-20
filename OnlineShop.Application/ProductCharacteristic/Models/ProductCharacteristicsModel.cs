using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.ProductCharacteristic.Models
{
    public class ProductCharacteristicsModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
