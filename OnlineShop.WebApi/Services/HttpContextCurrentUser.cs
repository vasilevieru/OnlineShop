using Microsoft.AspNetCore.Http;
using OnlineShop.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.WebApi.Services
{
    public class HttpContextCurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextCurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int UserId
        {
            get
            {
                HttpContext httpContext = _httpContextAccessor.HttpContext;
                var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
                int.TryParse(claimsIdentity.FindFirst("Id")?.Value, out var userId);
                return userId;
            }
        }
    }
}
