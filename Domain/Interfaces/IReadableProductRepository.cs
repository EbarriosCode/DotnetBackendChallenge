using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReadableProductRepository
    {
        Task<Product> GetByIdAsync(int id);
    }
}
