using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Create
{
    public record CreateProductCommand(
        string name, string sku, bool status, int stock, string description, decimal price
    ) : IRequest<Unit>;
}
