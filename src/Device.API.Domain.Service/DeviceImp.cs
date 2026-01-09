using Device.API.Domain.Contracts;
using Device.API.Domain.Contracts.Repositories;
using Device.API.Domain.Models.Entities;
using Device.API.Domain.Models.Model;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Device.API.Infrastructure.DI")]
namespace Device.API.Domain.Service
{
    internal class DeviceImp(IDeviceRepository deviceRepository) : IDeviceCRUD
    {
        private readonly IDeviceRepository _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));

        public Task<bool> CreateDeviceAsync(string name, string brand)
        {
            var entity = new DeviceEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                Brand = brand,
                State = State.Active,
                CreationTime = DateTime.UtcNow
            };

            return _deviceRepository.CreateAsync(entity);
        }

        public async Task<bool> DeleteDeviceAsync(Guid id)
        {
            return await _deviceRepository.DeleteAsync(id);
        }

        public async Task<List<DeviceEntity>> GetAllDevicesAsync()
        {
            return await _deviceRepository.GetAllAsync();
         }

        public async Task<DeviceEntity> GetDeviceByIdAsync(Guid id)
        {
            return await _deviceRepository.GetAsync(id);
        }

        public Task PatchDeviceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDeviceAsync(string name, string brand)
        {
            throw new NotImplementedException();
        }
    }
}
