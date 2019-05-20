using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Users;
using OnlineShop.Application.Users.Commands;
using OnlineShop.Application.Users.Queries;

namespace OnlineShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetUserDetailsQuery { Id = id }));
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterUserCommand command)
        {
            var user = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
        {
            var user = await Mediator.Send(query);

            return Ok(new { accessToken = user.Token, refreshToken = user.RefreshToken });
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenQuery refreshTokenQuery)
        {
            var user = await Mediator.Send(refreshTokenQuery);

            return Ok(new { accessToken = user.AccessToken, refreshToken = user.RefreshToken });
        }               
    }
}
