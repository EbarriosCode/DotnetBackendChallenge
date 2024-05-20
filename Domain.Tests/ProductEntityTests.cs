using Domain.Entities;

namespace Domain.Tests
{
    public class ProductEntityTests
    {
        [Theory]
        [InlineData(700.50, 20, 560.40)]
        [InlineData(1100, 50, 550)]
        [InlineData(800.75, 85, 120.1125)]
        public void Testing_Calculate_Final_Price(decimal price, int discount, decimal finalPriceExpected)
        {
            var product = new Product()
            {
                Name = "Logitech Mouse",
                Status = 0,
                Stock = 0,
                Description = "Ergonomic Mouse Logitech 2025",
                Price = price,
                Discount = discount
            };

            Assert.Equal(finalPriceExpected, product.CalculateFinalPrice());
        }
    }
}