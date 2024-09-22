using Business.Services;
using Business.Services.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {           
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
