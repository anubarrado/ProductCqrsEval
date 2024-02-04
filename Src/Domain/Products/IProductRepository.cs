using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(ProductId id);
        Task Add(Product product);
    }
}
