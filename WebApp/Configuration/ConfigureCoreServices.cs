using ApplicationCore;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infraestructure.Data;
using Infraestructure.Loggin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(MyRepository<>));

            services.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<ApplicationSettings>()));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
