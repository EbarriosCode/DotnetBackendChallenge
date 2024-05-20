using Application.DTOs.Request;
using Application.Exceptions;
using Application.Interfaces;
using FluentValidation;

namespace Application.Services
{
    public class UpdateProductRequestValidationService : IValidationService<UpdateProductRequestDTO>
    {
        private readonly IValidator<UpdateProductRequestDTO> _validator;

        public UpdateProductRequestValidationService(IValidator<UpdateProductRequestDTO> validator)
        {
            _validator = validator;
        }

        public void Validate(UpdateProductRequestDTO request)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Validation failed", validationResult.ToDictionary());
            }
        }
    }
}
