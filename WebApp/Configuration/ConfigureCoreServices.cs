using ApplicationCore;
using ApplicationCore.Entities.Validator;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using FluentValidation.AspNetCore;
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
            services.AddScoped(typeof(MyRepository<>));

            services.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<ApplicationSettings>()));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddMvc()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AlumnoValidator>());

            return services;
        }
    }
}
