using OnlineShop.Application.Users.Models;

namespace OnlineShop.Application.Interfaces
{
    public interface IAccessTokenGenerator
    {
        string GenerateAccessToken(string secret, UserLoginModel user);
    }
}
