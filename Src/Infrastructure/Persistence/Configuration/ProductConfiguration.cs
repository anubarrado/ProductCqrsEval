using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(
                producId => producId.value,
                value => new ProductId(value));
            
            builder.Property(p => p.Sku).HasMaxLength(10);
            builder.HasIndex(p => p.Sku).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(255);
            builder.Property(p => p.Status);
            builder.Property(p => p.Stock);
            builder.Property(p => p.Description).HasMaxLength(1024);
            builder.Ignore(p => p.Discount);
            builder.Ignore(p => p.FinalPrice);
        }
    }
}
