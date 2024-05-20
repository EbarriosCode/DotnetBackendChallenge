using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;

namespace Application.Commands.Products.Update
{
    public record UpdateProductCommand(UpdateProductRequestDTO Product) : IRequest<ProductResponseDTO>
    {
    }
}
