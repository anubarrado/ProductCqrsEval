using Application.Products.Create;
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
                customer => Created(),
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
