using Application.Commands.Products.Update;
using Application.DTOs.Request;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Application.Tests.Commands
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IValidationService<UpdateProductRequestDTO>> _validationServiceMock;

        public UpdateProductCommandHandlerTests()
        {
            this._productRepositoryMock = new Mock<IProductRepository>();
            this._validationServiceMock = new Mock<IValidationService<UpdateProductRequestDTO>>();
        }

        [Fact]
        public async Task UpdateProductCommandHandler()
        {
            this._validationServiceMock.Setup(x => x.Validate(It.IsAny<UpdateProductRequestDTO>()));
            this._productRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                                 .Returns(Task.FromResult(new Product
                                 {
                                     Name = "Laptop Dell",
                                     Status = 1,
                                     Stock = 100,
                                     Description = "Laptop Dell Gaming GT-X",
                                     Price = 7500.25m
                                 }));

            var request = new UpdateProductRequestDTO()
            {
                Name = "Laptop Dell",
                Status = 1,
                Stock = 100,
                Description = "Laptop Dell Gaming GT-X",
                Price = 7500.25m
            };
            var command = new UpdateProductCommand(request);

            var commandHandler = new UpdateProductCommandHandler(this._productRepositoryMock.Object, this._validationServiceMock.Object);
            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Status, result.Status);
            Assert.Equal(request.Stock, result.Stock);
            Assert.Equal(request.Description, result.Description);
            Assert.Equal(request.Price, result.Price);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenValidationFails()
        {
            var command = new UpdateProductCommand(new UpdateProductRequestDTO());

            this._validationServiceMock
                .Setup(service => service.Validate(command.Product))
                .Throws(new BadRequestException("Validation failed", new Dictionary<string, string[]>()));

            var handler = new UpdateProductCommandHandler(this._productRepositoryMock.Object, this._validationServiceMock.Object);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
