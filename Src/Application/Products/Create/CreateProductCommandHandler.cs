﻿using Domain.DomainErrors;
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

namespace Application.Products.Create
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Unit>>
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
                
                if (await _productRepository.GetBySkuAsync(command.sku) != null)
                {
                    return ErrorDomain.ErrorProduct.SkuExist;
                }

                var product = new Product(command.name, productSku, command.status, productStock, command.description, productPrice);

                _productRepository.Add(product);
                await _unitOfWork.SaveChangesAsync();
                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Error.Failure("CreateProduct.Failure", ex.Message + (ex.InnerException == null ? "" : " - " + ex.InnerException?.Message));
            }
        }
    }
}
