﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Users.Models
{
    public class RefreshTokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
