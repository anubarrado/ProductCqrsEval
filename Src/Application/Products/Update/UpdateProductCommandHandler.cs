using Domain.DomainErrors;
using Domain.Primitives;
using Domain.Products;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Update
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Unit>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (ProductPrice.Create(command.price) is not ProductPrice productPrice)
                {
                    return ErrorDomain.ErrorProduct.PriceInvalid;
                }

                if (ProductStock.Create(command.stock) is not ProductStock productStock)
                {
                    return ErrorDomain.ErrorProduct.StockInvalid;
                }

                if (ProductSku.Create(command.sku) is not ProductSku productSku)
                {
                    return ErrorDomain.ErrorProduct.SkuInvalid;
                }

                var product = new Product(command.id, command.name, productSku, command.status, productStock, command.description, productPrice);

                _productRepository.Update(product);
                await _unitOfWork.SaveChangesAsync();
                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Error.Failure("UpdateProduct.Failure", ex.Message + (ex.InnerException == null ? "" : " - " + ex.InnerException?.Message));
            }
        }
    }
}
