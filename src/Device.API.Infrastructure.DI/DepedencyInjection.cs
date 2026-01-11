using Device.API.Application.Service;
using Device.API.Application.Service.Interfaces;
using Device.API.Domain.Contracts;
using Device.API.Domain.Contracts.Repositories;
using Device.API.Domain.Service;
using Device.API.Infrastructure.Data.Context;
using Device.API.Infrastructure.Data.Interfaces;
using Device.API.Infrastructure.Data.Repositories;
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
            services.AddScoped<IDevicesOperation, DeviceOperation>();
            services.AddScoped<IDeviceCRUD, DeviceImp>();
            #endregion

            #region Infrastructure db context
            services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(
                    configuration.GetConnectionString("Postgres"), // ToDO : Move connection string to secret manager AWS secret or Azure Key vault whaetever
                    b => b.MigrationsAssembly("Device.API.Infrastructure.Data"))); //Configure EF Migrations Assembly 

            services.AddScoped<IDbContext, DBContext>(); //Manual db context to handle npgsql connections
            services.AddScoped<IDeviceRepository, DeviceRepositoryImp>();
            #endregion

            return services;
        }
    }
}
