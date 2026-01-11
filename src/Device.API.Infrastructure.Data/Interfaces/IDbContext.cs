using Npgsql;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Device.API.Infrastructure.DI")]
namespace Device.API.Infrastructure.Data.Interfaces
{
    public interface IDbContext
    {
        Task<NpgsqlConnection> GetConnectionAsync();
    }
}
