﻿using Application.Common.Interfaces;
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
        private readonly IDiscountService _discountService;
        private readonly ICacheService _cacheService;

        public GetProductByIdQueryHandler(IProductRepository ProductRepository, IDiscountService discountService, ICacheService cacheService)
        {
            _ProductRepository = ProductRepository ?? throw new ArgumentNullException(nameof(ProductRepository));
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
            _cacheService = cacheService;
        }

        public async Task<ErrorOr<ProductByIdResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _ProductRepository.GetByIdAsync(query.id) is not Product product)
            {
                return ErrorProduct.NotFound;
            }
                        
            var discount = await _discountService.GetDiscountByProductId(query.id);
            string statusName = _cacheService.GetCacheByKey(product.Status);

            product.SetDiscount(discount == null ? 0 : discount.Value);
            product.SetStatusName(statusName);

            return new ProductByIdResponse(
                product.Id,
                product.Name,
                product.Sku.Value,
                product.StatusName,
                product.Stock.Value,
                product.Description,
                product.Price.Value,
                product.Discount,
                product.FinalPrice
                );
        }
    }
}
