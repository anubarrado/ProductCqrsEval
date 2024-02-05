﻿using Application.Products.Response;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.GetById
{
    public record GetProductByIdQuery(int id) : IRequest<ErrorOr<ProductByIdResponse>>;
}
