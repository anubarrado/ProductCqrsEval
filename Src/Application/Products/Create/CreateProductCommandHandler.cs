﻿using Domain.Primitives;
using Domain.Products;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Create
{
    internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Unit>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (ProductPrice.Create(command.price) is not ProductPrice productPrice)
                {
                    return Error.Validation("Product.Price", "Price is not valid ");
                    //throw new ArgumentException(nameof(productPrice));
                }

                if (productPrice is null)
                    return Error.Validation("Product.Price", "Price has not a valid format");

                var product = new Product(command.name, command.sku, command.status, command.stock, command.description, productPrice);

                await _productRepository.Add(product);
                await _unitOfWork.SaveChangesAsync();
                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Error.Failure("CreateProduct.Failure", ex.Message);
            }
        }
    }
}
