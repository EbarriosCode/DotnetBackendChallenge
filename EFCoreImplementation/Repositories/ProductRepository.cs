using Domain.Entities;
using Domain.Interfaces;
using EFCoreImplementation.DataContext;
using Microsoft.EntityFrameworkCore;

namespace EFCoreImplementation.Repositories
{
    public class ProductRepository(EFCoreImplementationDbContext dbContext) : IProductRepository
    {
        private readonly EFCoreImplementationDbContext _dbContext = dbContext;

        public async Task<Product> GetByIdAsync(int productId)
        {
            if (productId == 0)
                return null;

            var product = await this._dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            return product;
        }

        public async Task<Product> InsertAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            await this._dbContext.Products.AddAsync(product);
            await this._dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            var productToUpdate = await this._dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId);

            if(productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Status = product.Status;
                productToUpdate.Stock = product.Stock;
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.Discount = product.Discount;   
                productToUpdate.FinalPrice = product.FinalPrice;

                await this._dbContext.SaveChangesAsync();
            }
            
            return productToUpdate;
        }
    }
}
