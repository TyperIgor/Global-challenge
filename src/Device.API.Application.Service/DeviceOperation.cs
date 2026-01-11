using Device.API.Application.Message.Dto;
using Device.API.Application.Message.Mapper;
using Device.API.Application.Service.Interfaces;
using Device.API.Domain.Contracts;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Device.API.Infrastructure.DI")]
namespace Device.API.Application.Service
{
    internal class DeviceOperation(IDeviceCRUD deviceCRUD) : IDevicesOperation
    {
        private readonly IDeviceCRUD _deviceCRUD = deviceCRUD ?? throw new ArgumentNullException(nameof(deviceCRUD));

        public async Task<bool> CreateAsync(string name, string brand)
        {
            return await _deviceCRUD.CreateDeviceAsync(name, brand);    
        }

        public async Task<bool> DeleteDeviceAsync(Guid id)
        {
            return await _deviceCRUD.DeleteDeviceAsync(id);
        }

        public async Task<ListDataResponse> GetAllAsync()
        {
            var response = await _deviceCRUD.GetAllDevicesAsync();

            return DeviceMapper.MapperToListResponse(response);
        }

        public async Task<ListDataResponse> GetByBrand(string brand)
        {
            var response = await _deviceCRUD.GetByBrand(brand);

            if (response is null)
                return null;

            return DeviceMapper.MapperToListResponse(response);
        }

        public async Task<SingleDataResponse> GetById(Guid id)
        {
            var response = await _deviceCRUD.GetDeviceByIdAsync(id);

            if (response is null)
                return null;

            return DeviceMapper.MapperToSingleResponse(response);
        }

        public async Task<ListDataResponse> GetByState(int state)
        {
            var response = await _deviceCRUD.GetByState(state);

            if (response is null)
                return null;

            return DeviceMapper.MapperToListResponse(response);
        }

        public async Task<bool> PartialOrFullUpdateAsync(DeviceUpdateRequest request)
        {
            var entity = DeviceMapper.MappertToDomainEntity(request);

            return await _deviceCRUD.PartialOrFullyUpdateDeviceAsync(entity);
        }
    }
}
