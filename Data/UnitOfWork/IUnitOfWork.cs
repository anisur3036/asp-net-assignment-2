using Inventory.Repositories.interfaces;

namespace Inventory.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        Task<int> SaveAsync();
    }
}