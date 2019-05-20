using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Users.Models
{
    public class ExchangeRefreshTokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
