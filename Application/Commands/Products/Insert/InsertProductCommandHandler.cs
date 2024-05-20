using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Extensions;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Products.Insert
{
    public class InsertProductCommandHandler : IRequestHandler<InsertProductCommand, ProductResponseDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidationService<InsertProductRequestDTO> _validationService;

        public InsertProductCommandHandler(IProductRepository productRepository, IValidationService<InsertProductRequestDTO> validationService)
        {
            this._productRepository = productRepository;
            this._validationService = validationService;
        }
        public async Task<ProductResponseDTO> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            this._validationService.Validate(request.Product);

            var newProductEntity = request.Product.ToEntity();
            await this._productRepository.InsertAsync(newProductEntity);

            return newProductEntity.ToDto();
        }
    }
}
