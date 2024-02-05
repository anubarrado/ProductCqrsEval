using Application.Products.Response;
using Domain.Products;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.DomainErrors.ErrorDomain;


namespace Application.Products.GetById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductByIdResponse>>
    {
        private readonly IProductRepository _ProductRepository;

        public GetProductByIdQueryHandler(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository ?? throw new ArgumentNullException(nameof(ProductRepository));
        }

        public async Task<ErrorOr<ProductByIdResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _ProductRepository.GetByIdAsync(query.id) is not Product product)
            {
                return ErrorProduct.NotFound;
            }

            return new ProductByIdResponse(
                product.Id,
                product.Name,
                product.Sku.Value,
                product.Status,
                product.Stock.Value,
                product.Description,
                product.Price.Value,
                product.Discount,
                product.FinalPrice
                );
        }
    }
}
