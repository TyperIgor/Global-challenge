using Device.API.Application.Service;
using Device.API.Application.Service.Interfaces;
using Device.API.Domain.Contracts;
using Device.API.Domain.Contracts.Repositories;
using Device.API.Domain.Service;
using Device.API.Infrastructure.Data.Context;
using Device.API.Infrastructure.Data.Interfaces;
using Device.API.Infrastructure.Data.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace Device.API.Infrastructure.DI
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {

            #region Application/Services Layers 
            services.AddScoped<IDevicesOperation, DeviceOperation>();
            services.AddScoped<IDeviceCRUD, DeviceImp>();
            #endregion
            #region FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            #endregion
            #region Infrastructure db context
            services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(
                    configuration.GetConnectionString("PostgresRender"), // ToDO : Move connection string to secret manager AWS secret or Azure Key vault whaetever
                    b => b.MigrationsAssembly("Device.API.Infrastructure.Data"))); //Configure EF Migrations Assembly 

            services.AddScoped<IDbContext, DBContext>(); //Manual db context to handle npgsql connections
            services.AddScoped<IDeviceRepository, DeviceRepositoryImp>();
            #endregion
            #region HealthCheck

            if (env.IsProduction())
            {
                services.AddHealthChecks()
                        .AddNpgSql(configuration.GetConnectionString("PostgresRender"), name: "Postgres Health Check");
            }

            services.AddHealthChecks()
                    .AddNpgSql(configuration.GetConnectionString("PostgresRender"), 
                        name: "Postgres Health Check",
                        tags: ["db", "ready"])
                    .AddCheck("api", () => HealthCheckResult.Healthy("API is running"));
            #endregion

            return services;
        }
    }
}
