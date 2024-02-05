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


namespace Application.Products.GetAll
{
    public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ErrorOr<List<ProductByIdResponse>>>
    {
        private readonly IProductRepository _ProductRepository;

        public GetAllProductsQueryHandler(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository ?? throw new ArgumentNullException(nameof(ProductRepository));
        }

        public async Task<ErrorOr<List<ProductByIdResponse>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            if (await _ProductRepository.GetAllAsync() is not List<Product> products)
            {
                return ErrorProduct.NotFound;
            }

            var productList = new List<ProductByIdResponse>();
            foreach (var product in products)
            {
                productList.Add(new ProductByIdResponse(
                    product.Id,
                    product.Name,
                    product.Sku.Value,
                    product.StatusName,
                    product.Stock.Value,
                    product.Description,
                    product.Price.Value,
                    product.Discount,
                    product.FinalPrice
                    ));
            }

            return productList;
        }
    }
}
