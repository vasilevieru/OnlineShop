using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(int size = 32);
    }
}
