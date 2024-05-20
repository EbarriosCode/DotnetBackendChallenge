using Application.Interfaces;
using Infraestructure.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Services
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUri = configuration["DiscountProductAPI"];

            services.AddHttpClient<IDiscountService, DiscountService>(client =>
            {
                client.BaseAddress = new Uri(baseUri);
            });

            return services;
        }
    }
}
