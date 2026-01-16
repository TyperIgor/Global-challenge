using Device.API.Domain.Contracts;
using Device.API.Domain.Contracts.Repositories;
using Device.API.Domain.Models.Entities;
using Microsoft.Extensions.Logging;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Device.API.Infrastructure.DI")]
namespace Device.API.Domain.Service
{
    internal class DeviceImp(IDeviceRepository deviceRepository, ILogger<DeviceImp> logger) : IDeviceCRUD // In this domain service class, i can use multiple repositories if needed and apply domain business rules
    {
        private readonly IDeviceRepository _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        private readonly ILogger<DeviceImp> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<bool> CreateDeviceAsync(string name, string brand)
        {
            _logger.LogInformation("Creating device with Name: {DeviceName}, Brand: {DeviceBrand}", name, brand);

            var entity = new DeviceEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                Brand = brand,
                State = State.Active,
                CreationTime = DateTime.UtcNow
            };

            var result = await _deviceRepository.CreateAsync(entity);

            if (result)
                _logger.LogInformation("Device created successfully. ID: {DeviceId}", entity.Id);
            else
                _logger.LogWarning("Failed to create device with Name: {DeviceName}, Brand: {DeviceBrand}", name, brand);

            return result;
        }

        public async Task<bool> DeleteDeviceAsync(Guid id)
        {
            _logger.LogInformation("Starting deletion process for device with ID: {DeviceId}", id);

            var checkDeviceInUse = await _deviceRepository.CheckDeviceExistAndIsInUse(id);

            if (checkDeviceInUse)
            {
                _logger.LogWarning("Device with ID: {DeviceId} is in use or does not exist. Deletion aborted.", id);
                return false;
            }

            var result = await _deviceRepository.DeleteAsync(id);

            if (result)
                _logger.LogInformation("Device with ID: {DeviceId} deleted successfully.", id);
            else
                _logger.LogWarning("Failed to delete device with ID: {DeviceId}.", id);

            return result;
        }

        public async Task<List<DeviceEntity>> GetAllDevicesAsync()
        {
            _logger.LogInformation("Retrieving all devices.");
            var devices = await _deviceRepository.GetAllAsync();
            _logger.LogInformation("Retrieved {DeviceCount} devices.", devices?.Count ?? 0);
            return devices;
        }

        public async Task<List<DeviceEntity>> GetByBrand(string brand)
        {
            _logger.LogInformation("Retrieving devices by brand: {DeviceBrand}", brand);
            var devices = await _deviceRepository.GetAllByBrand(brand);
            _logger.LogInformation("Retrieved {DeviceCount} devices for brand: {DeviceBrand}", devices?.Count ?? 0, brand);
            return devices;
        }

        public async Task<List<DeviceEntity>> GetByState(int state)
        {
            _logger.LogInformation("Retrieving devices by state: {DeviceState}", state);
            var devices = await _deviceRepository.GetAllByState(state);
            _logger.LogInformation("Retrieved {DeviceCount} devices for state: {DeviceState}", devices?.Count ?? 0, state);
            return devices;
        }

        public async Task<DeviceEntity> GetDeviceByIdAsync(Guid id)
        {
            _logger.LogInformation("Retrieving device by ID: {DeviceId}", id);
            var device = await _deviceRepository.GetAsync(id);
            if (device != null)
                _logger.LogInformation("Device found with ID: {DeviceId}", id);
            else
                _logger.LogWarning("Device not found with ID: {DeviceId}", id);
            return device;
        }

        public async Task<bool> PartialOrFullyUpdateDeviceAsync(DeviceEntity deviceToUpdate)
        {
            _logger.LogInformation("Updating device with ID: {DeviceId}", deviceToUpdate.Id);

            var checkDeviceExist = await _deviceRepository.CheckOnlyDeviceExist(deviceToUpdate.Id);

            if (!checkDeviceExist)
            {
                _logger.LogWarning("Device with ID: {DeviceId} does not exist. Update aborted.", deviceToUpdate.Id);
                return false;
            }

            var deviceOnDb = await _deviceRepository.GetAsync(deviceToUpdate.Id);

            if (deviceOnDb is null)
                return false;

            if (deviceOnDb.State == State.InUse)
            {
                if (deviceToUpdate.State is not null)
                    deviceOnDb.State = deviceToUpdate.State;

                await _deviceRepository.PartiallyOrFullyUpdateAsync(deviceToUpdate);

                _logger.LogInformation("Device with ID: {DeviceId} updated successfully.", deviceToUpdate.Id);

                return true;
            }

            if (deviceToUpdate.Name is not null)
                deviceOnDb.Name = deviceToUpdate.Name;

            if (deviceToUpdate.Brand is not null)
                deviceOnDb.Brand = deviceToUpdate.Brand;

            if (deviceToUpdate.State is not null)
                deviceOnDb.State = deviceToUpdate.State;

            var result = await _deviceRepository.PartiallyOrFullyUpdateAsync(deviceToUpdate);

            if (result)
                _logger.LogInformation("Device with ID: {DeviceId} updated successfully.", deviceToUpdate.Id);
            else
                _logger.LogWarning("Failed to update device with ID: {DeviceId}.", deviceToUpdate.Id);

            return result;
        }
    }
}
