﻿using OnlineShop.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Core.Domain
{
    public class Order : BaseEntity
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public double TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
