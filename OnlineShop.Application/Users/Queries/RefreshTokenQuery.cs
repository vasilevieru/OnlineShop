using MediatR;
using OnlineShop.Application.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Users.Queries
{
    public class RefreshTokenQuery : IRequest<RefreshTokenModel>
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
