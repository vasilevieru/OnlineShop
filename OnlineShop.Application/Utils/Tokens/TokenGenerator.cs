using OnlineShop.Application.Interfaces;
using System;
using System.Security.Cryptography;

namespace OnlineShop.Application.Utils.Tokens
{
    public sealed class TokenGenerator : ITokenGenerator
    {
        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
