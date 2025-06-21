using System.Linq.Expressions;
using Inventory.Models;
using Inventory.Models.Entities;

namespace Inventory.Repositories.interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<IEnumerable<Product>> GetFilterdProductsAsync(string searchName, string categoryFilter);
        Task<Product?> GetProductByIdAsync(int? productId);
        Task<IEnumerable<Product>> FindAllProductsAsync(Expression<Func<Product, bool>> predicate);
        void AddProduct(ProductCreateViewModel product);
        void UpdateProduct(EditProductViewModel product);
        void DeleteProduct(Product product);
    }
}