using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Models.Entities;
using Inventory.Repositories.interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services.Implements
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        public void DeleteProduct(Product product)
        {
            _productRepository.DeleteProduct(product);
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetFilterdProductsAsync(string searchName, string categoryFilter)
        {
            return await _productRepository.GetFilterdProductsAsync(searchName, categoryFilter);
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }
    }
}