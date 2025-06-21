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
        private readonly DbSet<Product> _products;

        public ProductRepository(InventoryDbContext context)
        {
            _products = context.Set<Product>();
        }
        public async void AddProduct(Product product)
        {
            product.CreatedDate = DateTime.Now;
            product.ModifiedDate = DateTime.Now;

            await _products.AddAsync(product);
        }

        public void DeleteProduct(Product product)
        {
            _products.Remove(product);
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
            IQueryable<Product> products = _products.Include(p => p.Category);
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

        public async Task<Product?> GetProductByIdAsync(int? productId)
        {
            return await _products.Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == productId);
        }

        public void UpdateProduct(Product product)
        {
            product.ModifiedDate = DateTime.Now;
            product.CreatedDate = _products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id)?.CreatedDate ?? DateTime.Now;

            _products.Update(product);
        }
    }
}