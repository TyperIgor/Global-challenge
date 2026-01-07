using Device.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Device.API.Infrastructure.DI
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region Application/Services Layers 
            #endregion

            #region Infrastructure db context
            services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(
                    configuration.GetConnectionString("Postgres"),
                    b => b.MigrationsAssembly("Device.API.Infrastructure.Data")));
            #endregion

            return services;
        }
    }
}
