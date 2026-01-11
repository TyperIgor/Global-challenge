using Device.API.Domain.Models.Entities;

namespace Device.API.Domain.Contracts
{
    public interface IDeviceCRUD
    {
        Task<List<DeviceEntity>> GetAllDevicesAsync();    

        Task<bool> CreateDeviceAsync(string name, string brand);

        Task<DeviceEntity> GetDeviceByIdAsync(Guid id);

        Task<List<DeviceEntity>> GetByBrand(string brand);

        Task<List<DeviceEntity>> GetByState(int state);

        Task<bool> PartialOrFullyUpdateDeviceAsync(DeviceEntity device);

        Task<bool> DeleteDeviceAsync(Guid id);
    }
}
