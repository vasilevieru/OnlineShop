﻿using MediatR;
using OnlineShop.Application.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Products.Commands
{
    public class UpdateProductCommand : IRequest<ProductDetailsModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
