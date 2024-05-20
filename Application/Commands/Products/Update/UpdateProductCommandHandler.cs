using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Extensions;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Products.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponseDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidationService<UpdateProductRequestDTO> _validationService;
        public UpdateProductCommandHandler(IProductRepository productRepository, IValidationService<UpdateProductRequestDTO> validationService)
        {
            this._productRepository = productRepository;
            this._validationService = validationService;
        }
        public async Task<ProductResponseDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            this._validationService.Validate(request.Product);

            var updatedProductEntity = request.Product.ToEntity();
            await this._productRepository.UpdateAsync(updatedProductEntity);

            return updatedProductEntity.ToDto();
        }
    }
}
