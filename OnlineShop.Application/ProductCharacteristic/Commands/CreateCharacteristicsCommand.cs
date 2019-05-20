using MediatR;
using OnlineShop.Application.ProductCharacteristic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.ProductCharacteristic.Commands
{
    public class CreateCharacteristicsCommand : IRequest<ProductCharacteristicsModel>
    {
        public int ProductId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
