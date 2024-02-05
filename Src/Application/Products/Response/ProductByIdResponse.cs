using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Response
{
    public record ProductByIdResponse
    {
        public int Id { get; private set; }
        public string Sku { get; set; }
        public string Name { get; private set; }
        public bool Status { get; private set; }
        public int Stock { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public decimal Discount { get; private set; }
        public decimal FinalPrice { get; private set; }

        public ProductByIdResponse(int id, string name, string sku, bool status, int stock, string description, decimal price, decimal discount, decimal finalPrice)
        {
            Id = id;
            Name = name;
            Sku = sku;
            Status = status;
            Stock = stock;
            Description = description;
            Price = price;
            Discount = discount;
            FinalPrice = finalPrice;
        }
    }
}
