using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainErrors
{
    public static partial class ErrorDomain
    {
        public static class ErrorProduct
        {
            public static Error PriceInvalid => Error.Validation("Product.Price", "Price is not valid ");
            public static Error StockInvalid => Error.Validation("Product.Stock", "Stock is not valid ");
            public static Error SkuInvalid => Error.Validation("Product.Sku", "SKU is not valid ");

            public static Error NotExist => Error.Validation("Product.NoExist", "The product not exist");

        }
    }
}
