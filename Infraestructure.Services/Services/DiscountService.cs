using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Infraestructure.Services.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public DiscountService(HttpClient httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
        }

        public async Task<int> GetDiscountAsync(int productId)
        {
            var endpoint = _configuration["DiscountProductEndpoint"];
            var response = await _httpClient.GetAsync($"{endpoint}/{productId}");
            response.EnsureSuccessStatusCode();

            var discountResponse = await response.Content.ReadFromJsonAsync<DiscountResponseDeserialized>();
            return discountResponse.Discount;
        }
    }
}
