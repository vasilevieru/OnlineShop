using AutoMapper;
using OnlineShop.Application.Pagination;
using OnlineShop.Application.Products.Commands;
using OnlineShop.Application.Products.Models;
using OnlineShop.Application.Products.Queries;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<ProductDetailsModel, Product>().ReverseMap();

            CreateMap<GetProductsPageQuery, PageDto>();
        }
    }
}
