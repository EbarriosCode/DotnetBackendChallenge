using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;

namespace Application.Commands.Products.Insert
{
    public record InsertProductCommand(InsertProductRequestDTO Product) : IRequest<ProductResponseDTO>
    {
    }
}
