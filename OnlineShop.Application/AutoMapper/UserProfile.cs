using AutoMapper;
using OnlineShop.Application.Users.Commands;
using OnlineShop.Application.Users.Models;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.Role, src => src.Ignore());

            CreateMap<UserDetailsModel, User>()
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForMember(dest => dest.PasswordSalt, src => src.Ignore())
                .ReverseMap();

            CreateMap<User, UserLoginModel>()
                .ForMember(dest => dest.Token, src => src.Ignore())
                .ForMember(dest => dest.RefreshToken, src => src.Ignore());

            CreateMap<RefreshToken, RefreshTokenModel>()
                .ForMember(dest => dest.RefreshToken, src => src.MapFrom(x => x.Token));

        }
        
    }
}
