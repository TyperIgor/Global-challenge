using Npgsql;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Device.API.Infrastructure.DI")]
namespace Device.API.Infrastructure.Data.Interfaces
{
    internal interface IDbContext
    {
        Task<NpgsqlConnection> GetConnectionAsync();
    }
}
