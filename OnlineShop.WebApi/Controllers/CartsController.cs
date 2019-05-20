using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Cart.Queries;

namespace OnlineShop.WebApi.Controllers
{
    [Route("api/carts")]
    public class CartsController : BaseController
    {
        private readonly IMediator _mediator;

        public CartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetItemCount()
        {
            return Ok(await Mediator.Send(new GetItemCountQuery()));
        }
        

    }
}