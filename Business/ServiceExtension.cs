using Business.Services;
using Business.Services.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Register your services with the DI container
            services.AddScoped<ICharacterService, CharacterService>();

            return services;
        }
    }
}
