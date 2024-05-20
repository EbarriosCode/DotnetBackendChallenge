using Infraestructure.Services.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace Infraestructure.Tests.Services
{
    public class DiscountServiceTests
    {
        [Fact]
        public async Task GetDiscountAsync_ReturnsDiscount()
        {
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(new DiscountResponseDeserialized { Discount = 10, ProductId = 1 })
            };
            var mockHttpMessageHandler = new HttpMessageHandlerMockForTests(responseMessage);
            var httpClient = new HttpClient(mockHttpMessageHandler)
            {
                BaseAddress = new Uri("http://fakebaseuri/")
            };

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config["DiscountProductEndpoint"]).Returns("fakeendpoint");

            var discountService = new DiscountService(httpClient, mockConfiguration.Object);

            var discount = await discountService.GetDiscountAsync(1);

            Assert.Equal(10, discount);
        }

        [Fact]
        public async Task GetDiscountAsync_ThrowsException_OnNonSuccessStatusCode()
        {
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };
            var mockHttpMessageHandler = new HttpMessageHandlerMockForTests(responseMessage);
            var httpClient = new HttpClient(mockHttpMessageHandler)
            {
                BaseAddress = new Uri("http://fakebaseuri/")
            };

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config["DiscountProductEndpoint"]).Returns("fakeendpoint");

            var discountService = new DiscountService(httpClient, mockConfiguration.Object);

            await Assert.ThrowsAsync<HttpRequestException>(() => discountService.GetDiscountAsync(1));
        }
    }
}
