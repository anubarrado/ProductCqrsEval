using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products
{
    public interface IProductRepository
    {
        Task<List<Product>?> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> GetBySkuAsync(string sku);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product id);
    }
}
