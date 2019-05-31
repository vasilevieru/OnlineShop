using OnlineShop.Domain.Entities;
using System.Collections.Generic;

namespace OnlineShop.Application.Products.Models
{
    public class ProductDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
