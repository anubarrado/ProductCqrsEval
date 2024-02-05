using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Update
{
    public record UpdateProductCommand(
        int id, string name, string sku, bool status, int stock, string description, decimal price
    ) : IRequest<ErrorOr<Unit>>;
}
