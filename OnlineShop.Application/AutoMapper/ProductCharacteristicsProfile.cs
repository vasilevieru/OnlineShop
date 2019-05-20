using AutoMapper;
using OnlineShop.Application.ProductCharacteristic.Commands;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.AutoMapper
{
    public class ProductCharacteristicsProfile : Profile
    {
        public ProductCharacteristicsProfile()
        {
            CreateMap<CreateCharacteristicsCommand, ProductCharacteristics>().ReverseMap();
        }
    }
}
