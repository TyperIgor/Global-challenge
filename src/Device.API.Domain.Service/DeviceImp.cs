using Device.API.Domain.Contracts;
using Device.API.Domain.Contracts.Repositories;
using Device.API.Domain.Models.Entities;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Device.API.Infrastructure.DI")]
namespace Device.API.Domain.Service
{
    internal class DeviceImp(IDeviceRepository deviceRepository) : IDeviceCRUD // In this domain service class, i can use multiple repositories if needed and apply domain business rules
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
            var checkDeviceInUse = await _deviceRepository.CheckDeviceExistAndIsInUse(id);

            if (checkDeviceInUse)
                return false;

            return await _deviceRepository.DeleteAsync(id);
        }

        public async Task<List<DeviceEntity>> GetAllDevicesAsync()
        {
            return await _deviceRepository.GetAllAsync();
        }

        public async Task<List<DeviceEntity>> GetByBrand(string brand)
        {
            return await _deviceRepository.GetAllByBrand(brand);
        }

        public async Task<List<DeviceEntity>> GetByState(int state)
        {
            return await _deviceRepository.GetAllByState(state);
        }

        public async Task<DeviceEntity> GetDeviceByIdAsync(Guid id)
        {
            return await _deviceRepository.GetAsync(id);
        }

        public async Task<bool> PartialOrFullyUpdateDeviceAsync(DeviceEntity device)
        {
            var checkDeviceExist = await _deviceRepository.CheckOnlyDeviceExist(device.Id);

            if (!checkDeviceExist)
                return false;

            return await _deviceRepository.PartiallyOrFullyUpdateAsync(device);
        }
    }
}
