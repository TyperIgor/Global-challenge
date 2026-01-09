using Device.API.Domain.Models.Entities;

namespace Device.API.Domain.Contracts.Repositories
{
    public interface IDeviceRepository
    {
        Task<List<DeviceEntity>> GetAllAsync();

        Task<DeviceEntity> GetAsync(Guid id);

        Task<List<DeviceEntity>> GetAllByBrand(string brand);

        Task<List<DeviceEntity>> GetAllByState(int state);

        Task<bool> CheckDeviceExistAndIsInUse(Guid id);

        Task<bool> CheckOnlyDeviceExist(Guid id);

        Task<bool> PartiallyOrFullyUpdateAsync(DeviceEntity id);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> CreateAsync(DeviceEntity device);
    }
}
