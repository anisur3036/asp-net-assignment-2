using Inventory.Data.UnitOfWork;
using Inventory.Models.Entities;
using Inventory.Services.Interfaces;

namespace Inventory.Services.Implements
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddProduct(Product product)
        {
            _unitOfWork.ProductRepository.AddProduct(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _unitOfWork.ProductRepository.DeleteProduct(product);
            await _unitOfWork.SaveAsync();
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetFilterdProductsAsync(string searchName, string categoryFilter)
        {
            return await _unitOfWork.ProductRepository.GetFilterdProductsAsync(searchName, categoryFilter);
        }

        public async Task<Product?> GetProductByIdAsync(int? productId)
        {
            return await _unitOfWork.ProductRepository.GetProductByIdAsync(productId);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _unitOfWork.ProductRepository.UpdateProduct(product);
            await _unitOfWork.SaveAsync();
        }
    }
}