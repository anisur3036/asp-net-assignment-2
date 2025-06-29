using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Inventory.Models;
using Inventory.Models.Entities;

namespace Inventory.Services.Interfaces
{
    public interface IProductServices
    {

        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetFilterdProductsAsync(string searchName, string categoryFilter);
        Task<Product?> GetProductByIdAsync(int? productId);
        Task AddProduct(ProductCreateViewModel product);
        Task UpdateProductAsync(EditProductViewModel product);
        Task DeleteProductAsync(Product product);
    }
}