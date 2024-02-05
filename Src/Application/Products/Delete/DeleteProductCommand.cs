using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Delete
{
    public record DeleteProductCommand(
        int id
    ) : IRequest<ErrorOr<Unit>>;
}
