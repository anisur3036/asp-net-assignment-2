using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Inventory.Data;
using Inventory.Models.Entities;
using Inventory.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;

        public ProductRepository(InventoryDbContext context)
        {
            _context = context;
        }
        public async Task AddProductAsync(Product product)
        {
            product.CreatedDate = DateTime.Now;
            product.ModifiedDate = DateTime.Now;

            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();
        }

        public async void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public Task<IEnumerable<Product>> FindAllProductsAsync(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetFilterdProductsAsync(string searchName, string categoryFilter)
        {
            IQueryable<Product> products = _context.Products.Include(p => p.Category);
            if (!string.IsNullOrEmpty(searchName))
            {
                products = products.Where(p => p.Name.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                products = products.Where(p => p.Category != null && p.Category.Name.Contains(categoryFilter));
            }

            return await products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == productId);
        }

        public async Task UpdateProductAsync(Product product)
        {
            product.ModifiedDate = DateTime.Now;
            product.CreatedDate = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id)?.CreatedDate ?? DateTime.Now;

            _context.Products.Update(product);

            await _context.SaveChangesAsync();
        }
    }
}