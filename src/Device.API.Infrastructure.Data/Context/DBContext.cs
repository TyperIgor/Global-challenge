using System.Data;
using Device.API.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Device.API.Infrastructure.Data.Context
{
    internal class DBContext : IDbContext
    {
        private NpgsqlConnection _npgsqlConnection;
        private readonly IConfiguration _configuration;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<NpgsqlConnection> GetConnectionAsync()
        {
            if (_npgsqlConnection == null || _npgsqlConnection.State == ConnectionState.Closed)
                return await CreateConnection();

            return _npgsqlConnection;
        }

        private async Task<NpgsqlConnection> CreateConnection()
        {
            _npgsqlConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresRender"));
            await _npgsqlConnection.OpenAsync();
            return _npgsqlConnection;
        }
    }
}
