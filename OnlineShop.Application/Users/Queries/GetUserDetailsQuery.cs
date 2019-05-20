using MediatR;
using OnlineShop.Application.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Users
{
    public class GetUserDetailsQuery : IRequest<UserViewModel>
    {
        public int Id { get; set; }
    }
}
