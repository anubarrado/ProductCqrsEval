using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.GetAll;
using Application.Products.GetById;
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
                product => Created(nameof(this.Create), null),
                errors => Problem(errors)
                );
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var updateResult = await _mediator.Send(command);

            return updateResult.Match(
                product => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpDelete]
        public async Task<IActionResult> Detele([FromBody] DeleteProductCommand command)
        {
            var deleteResult = await _mediator.Send(command);

            return deleteResult.Match(
                product => NoContent(),
                errors => Problem(errors)
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var productResult = await _mediator.Send(new GetProductByIdQuery(id));

            return productResult.Match(
                product => Ok(product),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productResult = await _mediator.Send(new GetAllProductsQuery());

            return productResult.Match(
                products => Ok(products),
                errors => Problem(errors)
            );
        }
    }
}
