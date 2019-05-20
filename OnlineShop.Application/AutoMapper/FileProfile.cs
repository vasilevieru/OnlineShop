using AutoMapper;
using OnlineShop.Application.Files.Commands;
using OnlineShop.Application.Files.Models;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.AutoMapper
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<Image, FileViewModel>();
            CreateMap<CreateFileCommand, Image>().ReverseMap();
        }
    }
}
