using Domain.Products;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationdbContext _context;

        public ProductRepository(ApplicationdbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public void Add(Product product) => _context.Products.Add(product);

        public async Task<List<Product>?> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetBySkuAsync(string sku)
        {
            var search = ProductSku.Create(sku);
            return await _context.Products.FirstOrDefaultAsync(p => p.Sku == search);
        }

        public void Update(Product product) => _context.Products.Update(product);
        
        public void Delete(Product product) => _context.Products.Remove(product);
    }
}
