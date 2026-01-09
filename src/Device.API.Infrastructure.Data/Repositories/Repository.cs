using Device.API.Infrastructure.Data.Interfaces;
using Npgsql;

namespace Device.API.Infrastructure.Data.Repositories
{
    internal abstract class Repository : IDisposable
    {
        protected readonly NpgsqlConnection _npgsqlConnection;
        protected Repository(IDbContext dbContext)
        {
            _npgsqlConnection = dbContext.GetConnectionAsync().GetAwaiter().GetResult();
        }

        public void Dispose() => _npgsqlConnection.DisposeAsync();

        public async Task CloseConnection() => await _npgsqlConnection.CloseAsync();
    }
}
