using Device.API.Domain.Models.Entities;

namespace Device.API.Domain.Contracts
{
    public interface IDeviceCRUD
    {
        Task<List<DeviceEntity>> GetAllDevicesAsync();    

        Task<bool> CreateDeviceAsync(string name, string brand);

        Task<DeviceEntity> GetDeviceByIdAsync(Guid id);

        Task<bool> UpdateDeviceAsync(string name, string brand);

        Task<bool> DeleteDeviceAsync(Guid id);

        Task PatchDeviceAsync();
    }
}
