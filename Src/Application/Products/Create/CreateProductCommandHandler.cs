using Domain.Primitives;
using Domain.Products;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Create
{
    internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Unit> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (ProductPrice.Create(command.price) is not ProductPrice productPrice)
            {
                throw new ArgumentException(nameof(productPrice));
            }

            var product = new Product(command.name, command.status, command.stock, command.description, productPrice);

            await _productRepository.Add(product);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
