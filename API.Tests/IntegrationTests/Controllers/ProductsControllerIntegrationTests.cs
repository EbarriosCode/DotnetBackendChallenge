using Application.DTOs.Request;
using Application.DTOs.Response;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace API.Tests.IntegrationTests.Controllers
{
    public class ProductsControllerIntegrationTests
    {
        private readonly WebAPIApplicationFactory _applicationFactory;
        private readonly HttpClient _client;

        public ProductsControllerIntegrationTests()
        {
            this._applicationFactory = new WebAPIApplicationFactory();
            this._client = this._applicationFactory.CreateClient();
        }

        [Fact]
        public async Task GetProductById_Should_Return_NotFound_When_Product_DoesNot_Exist_In_Database()
        {           
            var response = await this._client.GetAsync("/api/Products/1000");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetProductById_Should_Return_StatusCode_OK_When_Product_Exist_In_Database()
        {
            var response = await this._client.GetAsync("/api/Products/1");
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProductById_Should_Return_One_Product_When_Product_Exist_In_Database()
        {
            var response = await this._client.GetAsync("/api/Products/1");
            var controllerResponse = await response.Content.ReadFromJsonAsync<ProductResponseDTO>();
            
            controllerResponse.ProductId.Should().BePositive();
        }

        [Fact]
        public async Task InsertProduct_Should_Return_BadRequest_When_Request_Is_Null()
        {
            InsertProductRequestDTO request = null;

            var response = await this._client.PostAsJsonAsync("/api/Products", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task InsertProduct_Should_Return_Created_When_Request_Is_Right()
        {
            var request = new InsertProductRequestDTO()
            {
                Name = "Product from integration test",
                Status = 1,
                Stock = 50,
                Description = "testing",
                Price = 5000.25m
            };

            var response = await this._client.PostAsJsonAsync("/api/Products", request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_Should_Return_BadRequest_When_Request_Is_Null()
        {
            InsertProductRequestDTO request = null;

            var response = await this._client.PutAsJsonAsync("/api/Products", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_Should_Return_NoContent_When_Request_Is_Right()
        {
            var request = new UpdateProductRequestDTO()
            {
                ProductId = 2,
                Name = "Product Updating from integration test",
                Status = 0,
                Stock = 75,
                Description = "Updating from integration test",
                Price = 5000.25m
            };

            var response = await this._client.PutAsJsonAsync("/api/Products", request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
