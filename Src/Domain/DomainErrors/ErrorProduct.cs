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
        public static class Product
        {
            public static Error PriceInvalid => Error.Validation("Product.Price", "Price is not valid ");
        }
    }
}
