using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(r => r.id)
                .NotNull()
                .WithName("Id");

            RuleFor(r => r.name)
                .NotEmpty()
                .MaximumLength(255)
                .WithName("Name");

            RuleFor(r => r.sku)
               .NotEmpty()
               .MaximumLength(10)
               .WithName("SKU");

            RuleFor(r => r.price)
               .GreaterThanOrEqualTo(0)
               .WithName("Price");

            RuleFor(r => r.status)
               .NotNull()
               .WithName("Status");

            RuleFor(r => r.stock)
               .GreaterThanOrEqualTo(0)
               .WithName("Stock");

            RuleFor(r => r.description)
              .NotEmpty()
              .MaximumLength(1024)
              .WithName("Description");
        }
    }
}
