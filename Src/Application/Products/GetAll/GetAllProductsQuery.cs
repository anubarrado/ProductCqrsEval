using Application.Products.Response;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.GetAll
{
    public record GetAllProductsQuery() : IRequest<ErrorOr<List<ProductByIdResponse>>>;
}
