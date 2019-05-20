using MediatR;
using OnlineShop.Application.Users.Models;

namespace OnlineShop.Application.Users.Commands
{
    public class RegisterUserCommand : IRequest<UserDetailsModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "user";
        public string Password { get; set; }

    }
}
