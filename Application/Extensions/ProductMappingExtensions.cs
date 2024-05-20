using Application.DTOs.Request;
using Application.DTOs.Response;
using Domain.Entities;

namespace Application.Extensions
{
    public static class ProductMappingExtensions
    {
        public static Product ToEntity(this InsertProductRequestDTO request)
        {
            return new Product
            {
                Name = request.Name,
                Status = request.Status,
                Stock = request.Stock,
                Description = request.Description,
                Price = request.Price
            };
        }

        public static Product ToEntity(this UpdateProductRequestDTO request)
        {
            return new Product
            {
                ProductId = request.ProductId,
                Name = request.Name,
                Status = request.Status,
                Stock = request.Stock,
                Description = request.Description,
                Price = request.Price
            };
        }

        public static ProductResponseDTO ToDto(this Product product)
        {
            return new ProductResponseDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Status = product.Status,
                Stock = product.Stock,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                FinalPrice = product.FinalPrice
            };
        }
    }
}
