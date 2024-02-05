using Domain.Primitives;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products
{
    public sealed class Product : AggregateRoot
    {
        public int Id { get; private set; }
        public ProductSku Sku { get; set; }
        public string Name { get; private set; } = string.Empty;
        public bool Status { get; private set; }
        public ProductStock Stock { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public ProductPrice Price { get; private set; }

        public decimal Discount { get; private set; }
        public decimal FinalPrice { get; private set; }

        public Product()
        {            
        }

        public Product(string name, ProductSku sku, bool status, ProductStock stock, string description, ProductPrice price)
        {
            Name = name;
            Sku = sku;
            Status = status;
            Stock = stock;
            Description = description;
            Price = price;
        }

        public Product(int id, string name, ProductSku sku, bool status, ProductStock stock, string description, ProductPrice price)
        {
            Id = id;
            Name = name;
            Sku = sku;
            Status = status;
            Stock = stock;
            Description = description;
            Price = price;
        }
    }
}
