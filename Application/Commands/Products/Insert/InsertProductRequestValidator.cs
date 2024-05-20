using Application.DTOs.Request;
using FluentValidation;

namespace Application.Commands.Products.Insert
{
    public class InsertProductRequestValidator : AbstractValidator<InsertProductRequestDTO>
    {
        public InsertProductRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Status)
                .Must(value => value == 0 || value == 1)
                .WithMessage("{PropertyName} must be either 0 or 1.");

            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}.");

            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        }
    }
}
