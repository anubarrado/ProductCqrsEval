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

namespace Application.Products.Delete
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<Unit>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (await _productRepository.GetByIdAsync(command.id) is not Product product)
                {
                    return ErrorDomain.ErrorProduct.NotExist;
                }

                _productRepository.Delete(product);
                await _unitOfWork.SaveChangesAsync();
                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Error.Failure("DeleteProduct.Failure", ex.Message + (ex.InnerException == null ? "" : " - " + ex.InnerException?.Message));
            }
        }
    }
}
