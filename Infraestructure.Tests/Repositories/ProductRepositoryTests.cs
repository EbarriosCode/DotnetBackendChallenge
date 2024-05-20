using Domain.Entities;
using EFCoreImplementation.Repositories;
using FluentAssertions;

namespace Infraestructure.Tests.Repositories
{
    public class ProductRepositoryTests : EFCoreDbContextBaseTest
    {
        [Fact]
        public async Task GetById_Method_Should_Return_Null_When_Parameter_Is_Zero()
        {
            var repository = new ProductRepository(null);

            var result = await repository.GetByIdAsync(0);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_Method_Should_Return_One_Product()
        {
            var repository = new ProductRepository(base._context);

            var result = await repository.GetByIdAsync(1);

            Assert.Equal(1, result.ProductId);
            Assert.Contains("Laptop", result.Name);
            Assert.Equal(1, result.Status);
            Assert.Equal(10, result.Stock);
            Assert.Contains("Gaming", result.Description);
            Assert.Equal(5565.25m, result.Price);
            Assert.Equal(0, result.Discount);
            Assert.Equal(5565.25m, result.FinalPrice);
        }

        [Fact]
        public async Task Insert_Method_Should_Throw_ArgumentNullException_When_Parameter_Is_Null()
        {
            var repository = new ProductRepository(null);

            Task result () => repository.InsertAsync(null);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(result);
            Assert.Equal("Value cannot be null. (Parameter 'product')", exception.Message);
        }

        [Fact]
        public async Task Insert_Method_Should_Return_The_Product_That_We_Are_Trying_Creating()
        {
            var repository = new ProductRepository(base._context);

            var product = new Product()
            {
                ProductId = 3,
                Name = "Test Product",
                Status = 1,
                Stock = 50,
                Description = "Test Description",
                Price = 250.7m,
                Discount = 0
            };

            var result = await repository.InsertAsync(product);

            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task Update_Method_Should_Throw_ArgumentNullException_When_Parameter_Is_Null()
        {
            var repository = new ProductRepository(null);

            Task result() => repository.UpdateAsync(null);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(result);
            Assert.Equal("Value cannot be null. (Parameter 'product')", exception.Message);
        }

        [Fact]
        public async Task Update_Method_Should_Return_The_Product_That_We_Are_Trying_Updating()
        {
            var repository = new ProductRepository(base._context);

            var product = new Product()
            {
                ProductId = 1,
                Name = "Test Updating Product",
                Status = 0,
                Stock = 50,
                Description = "Test Updating Description",
                Price = 250.7m,
                Discount = 0,
                FinalPrice = 5565.25m
            };

            var result = await repository.UpdateAsync(product);

            result.Should().BeEquivalentTo(product);
        }
    }
}
