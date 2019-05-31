using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.ProductCharacteristic.Commands;
using OnlineShop.Application.Products.Commands;
using OnlineShop.Application.Products.Queries;

namespace OnlineShop.WebApi.Controllers
{
    [Route("api/products")]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] GetProductsPageQuery pageQuery)
        {
            return Ok(await Mediator.Send(pageQuery));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetProductDetailsQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var product = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("{id}/characteristics")]
        public async Task<IActionResult> CreateCharacteristics([FromBody] CreateCharacteristicsCommand command)
        {
            var characteristic = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { characteristic.Id }, characteristic);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteProductCommand { Id = id }));
        }
    }
}