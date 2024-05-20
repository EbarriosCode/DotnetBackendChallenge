using Domain.Entities;
using EFCoreImplementation.DataContext;

namespace Infraestructure.Tests
{
    public class DbInitializerForTests
    {
        public static void Initialize(EFCoreImplementationDbContext context)
        {
            if (context.Products.Any())
                return;

            Seed(context);
        }

        private static void Seed(EFCoreImplementationDbContext context)
        {
            var products = new[]
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Laptop Dell Gaming Pro-x3",
                    Status = 1,
                    Stock = 10,
                    Description = "Gaming",
                    Price = 5565.25m,
                    Discount = 0,
                    FinalPrice = 5565.25m
                },
                new Product()
                {
                    ProductId = 2,
                    Name = "Logitech Mouse",
                    Status = 0,
                    Stock = 0,
                    Description = "Ergonomic Mouse Logitech 2025",
                    Price = 700.50m,
                    Discount = 20,
                    FinalPrice = 560.40m
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
