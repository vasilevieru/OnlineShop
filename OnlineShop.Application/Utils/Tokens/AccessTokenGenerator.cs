using Microsoft.IdentityModel.Tokens;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.Users.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShop.Application.Utils.Tokens
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        public string GenerateAccessToken(string secret, UserLoginModel user)
        {
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>();
            claims.Add(new Claim("Roles", user.Role));
            claims.Add(new Claim("FullName", user.FirstName + " " + user.LastName));
            claims.Add(new Claim("Id", user.Id.ToString()));

            var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(4),
                    signingCredentials: signingCredentials,
                    claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
