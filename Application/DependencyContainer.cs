using Application.DTOs.Request;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {           
            services.AddScoped<IValidationService<InsertProductRequestDTO>, InsertProductRequestValidationService>();
            services.AddScoped<IValidationService<UpdateProductRequestDTO>, UpdateProductRequestValidationService>();

            return services;
        }
    }
}
