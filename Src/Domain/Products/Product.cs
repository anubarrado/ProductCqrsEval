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
        public ProductId Id { get; private set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public bool Status { get; private set; }
        public int Stock { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public ProductPrice Price { get; private set; }


        public decimal Discount { get; private set; }
        public decimal FinalPrice { get; private set; }

        public Product()
        {            
        }

        public Product(string name, bool status, int stock, string description, ProductPrice price)
        {
            Name = name;
            Status = status;
            Stock = stock;
            Description = description;
            Price = price;
        }
    }
}
