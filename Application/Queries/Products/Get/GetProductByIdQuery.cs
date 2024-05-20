using Application.DTOs.Response;
using MediatR;

namespace Application.Queries.Products.Get
{
    public record GetProductByIdQuery(int ProductId) : IRequest<ProductResponseByIdDTO>
    {
    }
}
