
using Device.API.Domain.Contracts.Repositories;
using Device.API.Infrastructure.Data.Interfaces;
using Device.API.Infrastructure.Data.Context;
using Device.API.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Device.API.Infrastructure.Data.Repositories
{
    internal class DeviceRepositoryImp(IDbContext dbContext, AppDbContext context) : Repository(dbContext), IDeviceRepository
    {
        private readonly AppDbContext _appDbContext = context ?? throw new ArgumentNullException(nameof(context));

        private readonly DbSet<DeviceEntity> _dbSet = context.Devices ?? throw new ArgumentNullException(nameof(context));

        public async Task<bool> CheckDeviceExistAndIsInUse(Guid id)
        {
            return await _dbSet.AsNoTracking().AnyAsync(x => x.Id == id && x.State == State.InUse);
        }

        public async Task<bool> CheckOnlyDeviceExist(Guid id)
        {
            return await _dbSet.AsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task<bool> CreateAsync(DeviceEntity device)
        {
            try
            {
                await _dbSet.AddAsync(device);

                var rows = await _appDbContext.SaveChangesAsync();

                if (rows <= 0)
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
                var row = await _dbSet.AsNoTracking()
                    .Where(x => x.Id == id)
                    .ExecuteDeleteAsync();

                if (row < 0)
                    throw new Exception("Error deleting device");

                return true;
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
                return await _appDbContext.Devices.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DeviceEntity>> GetAllByBrand(string brand)
        {
            try
            {
                return await _dbSet.AsNoTracking()
                        .Where(x => x.Brand == brand)
                        .ToListAsync();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<DeviceEntity>> GetAllByState(int state)
        {
            try
            {
                return await _dbSet.AsNoTracking()
                        .Where(x => (int)x.State == state)
                        .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DeviceEntity> GetAsync(Guid id)
        {
            try
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> PartiallyOrFullyUpdateAsync(DeviceEntity entityToUpdate)
        {
            try
            {
                var deviceOnDb = await _dbSet.FindAsync(entityToUpdate.Id);

                if (deviceOnDb is null)
                    return false;

                if (deviceOnDb.State == State.InUse) 
                {
                    if (entityToUpdate.State is not null)
                        deviceOnDb.State = entityToUpdate.State;

                    await _appDbContext.SaveChangesAsync();

                    return true;
                }

                if (entityToUpdate.Name is not null)
                    deviceOnDb.Name = entityToUpdate.Name;

                if (entityToUpdate.Brand is not null)
                    deviceOnDb.Brand = entityToUpdate.Brand;

                if (entityToUpdate.State is not null)
                    deviceOnDb.State = entityToUpdate.State;

                await _appDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
