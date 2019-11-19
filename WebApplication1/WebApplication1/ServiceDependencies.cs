using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductStore.Entities.Repos;
using ProductStore.Services.Interfaces;
using ProductStore.Services.Options;
using ProductStore.Services.Services;

namespace WebApplication1
{
    public class ServiceDependencies
    {
        public static void SetupServiceDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
