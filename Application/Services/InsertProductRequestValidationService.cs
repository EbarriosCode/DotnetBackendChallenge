using Application.DTOs.Request;
using Application.Exceptions;
using Application.Interfaces;
using FluentValidation;

namespace Application.Services
{
    public class InsertProductRequestValidationService : IValidationService<InsertProductRequestDTO>
    {
        private readonly IValidator<InsertProductRequestDTO> _validator;

        public InsertProductRequestValidationService(IValidator<InsertProductRequestDTO> validator)
        {
            _validator = validator;
        }

        public void Validate(InsertProductRequestDTO request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Validation failed", validationResult.ToDictionary());
            }
        }
    }
}
