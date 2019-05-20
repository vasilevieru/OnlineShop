using MediatR;
using OnlineShop.Application.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Users.Queries
{
    public class LoginUserQuery : IRequest<UserLoginModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
