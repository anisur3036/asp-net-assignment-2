using Inventory.Repositories.interfaces;

namespace Inventory.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;
        public UnitOfWork(
            IProductRepository productRepository,
            InventoryDbContext context)
        {
            _context = context;
            ProductRepository = productRepository;
        }
        public IProductRepository ProductRepository { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}