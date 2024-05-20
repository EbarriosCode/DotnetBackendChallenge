using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IWritableProductRepository
    {
        Task<Product> InsertAsync(Product product);
        Task<Product> UpdateAsync(Product product);
    }
}
