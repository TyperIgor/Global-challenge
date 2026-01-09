
using Device.API.Domain.Contracts.Repositories;
using Device.API.Infrastructure.Data.Interfaces;
using Device.API.Infrastructure.Data.Context;
using Dapper.Contrib.Extensions;
using Device.API.Domain.Models.Entities;

namespace Device.API.Infrastructure.Data.Repositories
{
    internal class DeviceRepositoryImp(IDbContext dbContext, AppDbContext context) : Repository(dbContext), IDeviceRepository
    {
        public async Task<bool> CreateAsync(DeviceEntity device)
        {
            try
            {
                var result = await _npgsqlConnection.InsertAsync(device);

                if (result < 0)
                    throw new Exception("Error inserting device");

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _npgsqlConnection.DeleteAsync(new DeviceEntity { Id = id });
                
                if (result is false)
                    throw new Exception("Error deleting device");

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<DeviceEntity>> GetAllAsync()
         {
            try
            {
                var result = await _npgsqlConnection.GetAllAsync<DeviceEntity>();

                 return result.ToList();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<DeviceEntity> GetAsync(Guid id)
        {
            try
            {
                return await _npgsqlConnection.GetAsync<DeviceEntity>(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
