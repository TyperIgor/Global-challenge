using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Device.API.Infrastructure.Logging
{
    public static class LogExtension
    {
        public static IServiceCollection ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console() 
                .CreateLogger();

            return services;
        }
    }
}
