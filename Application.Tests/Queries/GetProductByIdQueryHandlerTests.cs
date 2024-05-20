using Application.Interfaces;
using Application.Queries.Products.Get;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Application.Tests.Queries
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IDiscountService> _discountService;
        private readonly Mock<IMemoryCache> _memoryCache;

        public GetProductByIdQueryHandlerTests()
        {
            this._productRepository = new Mock<IProductRepository>();
            this._discountService = new Mock<IDiscountService>();
            this._memoryCache = new Mock<IMemoryCache>();
        }

        [Fact]
        public async Task GetProductByIdQueryHandlerSuccess()
        {
            var expectedDictionary = new Dictionary<int, string>
            {
                { 0, "Inactive" },
                { 1, "Active" }
            };

            object outValue = expectedDictionary;
            this._memoryCache
                .Setup(mc => mc.TryGetValue("StatusProductDictionary", out outValue))
                .Returns(true);

            var productExpected = new Product()
            {
                Name = "Laptop Dell",
                Status = 1,
                Stock = 100,
                Description = "Laptop Dell Gaming GT-X",
                Price = 8400.80m
            };

            this._productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                                   .Returns(Task.FromResult(productExpected));

            this._discountService.Setup(x => x.GetDiscountAsync(It.IsAny<int>())).Returns(Task.FromResult(50));

            var query = new GetProductByIdQuery(2);
            var queryHandler = new GetProductByIdQueryHandler(this._productRepository.Object, this._discountService.Object, this._memoryCache.Object);
            var queryResult = await queryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(productExpected.Name, queryResult.Name);
            Assert.Equal("Active", queryResult.StatusName);
            Assert.Equal(productExpected.Stock, queryResult.Stock);
            Assert.Equal(productExpected.Description, queryResult.Description);
            Assert.Equal(productExpected.Price, queryResult.Price);
            Assert.Equal(50, queryResult.Discount);
            Assert.Equal(4200.40m, queryResult.FinalPrice);
        }
    }
}
