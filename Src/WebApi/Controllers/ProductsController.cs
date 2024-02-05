using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiController
    {
        private readonly ISender _mediator;

        public ProductsController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var createResult = await _mediator.Send(command);

            return createResult.Match(
                customer => Created(nameof(this.Create), null),
                errors => Problem(errors)
                );
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var createResult = await _mediator.Send(command);

            return createResult.Match(
                customer => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete]
        public async Task<IActionResult> Detele([FromBody] DeleteProductCommand command)
        {
            var createResult = await _mediator.Send(command);

            return createResult.Match(
                customer => NoContent(),
                errors => Problem(errors)
                );
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
