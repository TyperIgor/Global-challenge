using Device.API.Domain.Models.Entities;

namespace Device.API.Domain.Contracts.Repositories
{
    public interface IDeviceRepository
    {
        Task<List<DeviceEntity>> GetAllAsync();

        Task<DeviceEntity> GetAsync(Guid id);

        Task<bool> UpdateAsync(int id);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> CreateAsync(DeviceEntity device);
    }
}
