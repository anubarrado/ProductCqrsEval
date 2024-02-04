using Domain.Products;
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

        public async Task Add(Product product) => await _context.Products.AddAsync(product);

        public async Task<Product?> GetByIdAsync(ProductId id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
