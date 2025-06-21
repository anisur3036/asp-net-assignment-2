using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Inventory.Models.Entities;

namespace Inventory.Repositories.interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<IEnumerable<Product>> GetFilterdProductsAsync(string searchName, string categoryFilter);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<IEnumerable<Product>> FindAllProductsAsync(Expression<Func<Product, bool>> predicate);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        void DeleteProduct(Product product);
    }
}