using Domain.Products;
using Domain.ValueObjects;
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

            builder.HasKey(x => x.Id).IsClustered(true);

            builder.Property(x => x.Id)
                //.HasConversion(
                //    producId => producId.value,
                //    value => new ProductId(value)
                //)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Price).HasConversion(
              producPrice => producPrice.Value,
              value => ProductPrice.Create(value));

            builder.Property(x => x.Stock).HasConversion(
             producStock => producStock.Value,
             value => ProductStock.Create(value));

            builder.Property(x => x.Sku).HasConversion(
              producSku => producSku.Value,
              value => ProductSku.Create(value));
            builder.HasIndex(p => p.Sku).IsUnique();

            builder.Property(p => p.Name).HasMaxLength(255);

            builder.Property(p => p.Status);

            builder.Property(p => p.Description).HasMaxLength(1024);

            builder.Ignore(p => p.StatusName);
            builder.Ignore(p => p.Discount);
            builder.Ignore(p => p.FinalPrice);            
        }
    }
}
