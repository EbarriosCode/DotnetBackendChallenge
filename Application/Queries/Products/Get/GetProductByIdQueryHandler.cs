using Application.DTOs.Response;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Queries.Products.Get
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponseByIdDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscountService _discountService;
        private readonly IMemoryCache _memoryCache;
        public GetProductByIdQueryHandler(IProductRepository productRepository, IDiscountService discountService, IMemoryCache memoryCache)
        {
            this._productRepository = productRepository;
            this._discountService = discountService;
            this._memoryCache = memoryCache;
        }

        public async Task<ProductResponseByIdDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var statusDictionaryFromCache = (Dictionary<int, string>)this._memoryCache.Get("StatusProductDictionary");

            var productFilteredById = await this._productRepository.GetByIdAsync(request.ProductId);

            if (productFilteredById == null)
                return null;

            var statusNameFromCache = statusDictionaryFromCache != null
                                        ? statusDictionaryFromCache[productFilteredById.Status]
                                        : "Status Not Available";

            var discountFromService = await this._discountService.GetDiscountAsync(productFilteredById.ProductId);
            productFilteredById.Discount = discountFromService;
            productFilteredById.CalculateFinalPrice();

            return new ProductResponseByIdDTO()
                    {
                        ProductId = productFilteredById.ProductId,
                        Name = productFilteredById.Name,
                        StatusName = statusNameFromCache,
                        Stock = productFilteredById.Stock,
                        Description = productFilteredById.Description,
                        Price = productFilteredById.Price,
                        Discount = productFilteredById.Discount,
                        FinalPrice = productFilteredById.FinalPrice
                    };
        }
    }
}
